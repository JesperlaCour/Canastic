using System;
using System.IO;
using System.Threading.Tasks;

namespace ImageProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            string rootPath = "C:\\Users\\jespe\\Documents\\Projects\\Canastic\\images";

            foreach (var folder in Directory.GetDirectories(rootPath))
            {
                foreach (var subfolder in Directory.GetDirectories(folder))
                {
                    foreach(var element in Directory.GetFiles(subfolder))
                    {

                        await ProcessImage(element);
                    }
                    
                }
            }
            Console.ReadLine();
        }

        private static Task ProcessImage(string element)
        {
             
        }
    }
}
