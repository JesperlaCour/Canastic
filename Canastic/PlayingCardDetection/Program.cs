using Microsoft.ML;
using Microsoft.ML.Vision;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Microsoft.ML.DataOperationsCatalog;

namespace PlayingCardDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));
            var workspaceRelativePath = Path.Combine(projectDirectory, "workspace");
            var assetsRelativePath = Path.Combine(projectDirectory, "assets");

            MLContext mlContext = new MLContext();

            IEnumerable<ImageData> images = LoadImagesFromDirectory(assetsRelativePath);

            IDataView imageData = mlContext.Data.LoadFromEnumerable(images);

            IDataView shuffledData = mlContext.Data.ShuffleRows(imageData);

            var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                                                inputColumnName: "Label",
                                                outputColumnName: "LabelAsKey")
                                                .Append(mlContext.Transforms.LoadRawImageBytes(
                                                outputColumnName: "Image",
                                                imageFolder: assetsRelativePath,
                                                inputColumnName: "ImagePath"));

            IDataView preProcessedData = preprocessingPipeline
                                        .Fit(shuffledData) 
                                        .Transform(shuffledData);

            TrainTestData trainSplit = mlContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.3);
            TrainTestData validationTestSplit = mlContext.Data.TrainTestSplit(trainSplit.TestSet);

            IDataView trainSet = trainSplit.TrainSet;
            IDataView validationSet = validationTestSplit.TrainSet;
            IDataView testSet = validationTestSplit.TestSet;

            var classifierOptions = new ImageClassificationTrainer.Options()
            {
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                ValidationSet = validationSet,
                Arch = ImageClassificationTrainer.Architecture.ResnetV2101,
                MetricsCallback = (metrics) => Console.WriteLine(metrics),
                TestOnTrainSet = false,
                ReuseTrainSetBottleneckCachedValues = true,
                ReuseValidationSetBottleneckCachedValues = true
            };

            var trainingPipeline = mlContext.MulticlassClassification.Trainers.ImageClassification(classifierOptions)
    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            ITransformer trainedModel = trainingPipeline.Fit(trainSet);

            ClassifySingleImage(mlContext, testSet, trainedModel);

            Console.ReadLine();

        }

        public static void ClassifySingleImage(MLContext mlContext, IDataView data, ITransformer trainedModel)
        {
            PredictionEngine<ModelInput, ModelOutput> predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);
            ModelInput image = mlContext.Data.CreateEnumerable<ModelInput>(data, reuseRowObject: true).First();
            ModelOutput prediction = predictionEngine.Predict(image);
            Console.WriteLine("Classifying single image");
            OutputPrediction(prediction);
        }

        private static void OutputPrediction(ModelOutput prediction)
        {
            string imageName = Path.GetFileName(prediction.ImagePath);
            Console.WriteLine($"Image: {imageName} | Actual Value: {prediction.Label} | Predicted Value: {prediction.PredictedLabel}");
        }


        public static IEnumerable<ImageData> LoadImagesFromDirectory(string folder, bool useFolderNameAsLabel = true)
        {
            var images = new List<ImageData>();
            var files = Directory.GetFiles(folder, "*", searchOption: SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if ((Path.GetExtension(file) != ".jpg") && (Path.GetExtension(file) != ".png") && (Path.GetExtension(file) != ".jpeg"))
                    continue;
                var label = Path.GetFileName(file);

                var dir = Directory.GetParent(file).ToString();

                if (useFolderNameAsLabel)
                    label = Directory.GetParent(dir).Name + Directory.GetParent(file).Name.ToUpper();
                else
                {
                    for (int index = 0; index < label.Length; index++)
                    {
                        if (!char.IsLetter(label[index]))
                        {
                            label = label.Substring(0, index);
                            break;
                        }
                    }
                }
                yield return new ImageData()
                {
                    ImagePath = file,
                    Label = label
                };
            }
        }

    }
}
