using GScraper;
using GScraper.Google;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;


//Project image scraping - failed
/* forsøg på at scrape billede data fra google. Kvaliteten af de billeder der kunne findes var ikke god nok
 og der var for stor udsving i om det returede afspejlede det søgte.
Medtaget i projektet, da det viser omstændighederne der kan være ved at skaffe data til sin machine learning*/



namespace ImageScraper
{
    class Program
    {
        private static GoogleScraper ImageScraper = new();
        public static IEnumerable<IImageResult> Images { get; set; }

        static async Task Main(string[] args)
        {

            string searchText = "playing card ";
            string rootPath = "C:\\Users\\jespe\\Documents\\Projects\\Canastic\\images";

            foreach (var folder in Directory.GetDirectories(rootPath))
            {

                searchText = searchText + new DirectoryInfo(folder).Name + " ";
                foreach (var subfolder in Directory.GetDirectories(folder))
                {
                    searchText = searchText + new DirectoryInfo(subfolder).Name;
                    await ScrapeImagesAndSave(searchText, subfolder);
                }
            }
            Console.ReadLine();
        }
        private static async Task ScrapeImagesAndSave(string searchText, string folderPath)
        {

            Images = await ImageScraper.GetImagesAsync(searchText);
            ImageFormat format = ImageFormat.Jpeg;
            if (Images != null)
            {
                foreach (var image in Images)
                {
                    try
                    {
                        saveFile(image, folderPath, format);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"error when downloading: {image.Title}, {image.Url}", ex);
                    }
                }
            }
        }

        private static void saveFile(IImageResult image, string folderPath, ImageFormat format)
        {
            
            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead(image.Url);
                Bitmap bitmap = new Bitmap(stream);

                if (bitmap != null)
                {
                    var path = (Path.Combine(folderPath, $"{image.Title}.jpg"));
                    
                    bitmap.Save(path, format);
                }

                
                stream.Flush();
                stream.Close();
            }
            
        }
    }
}
