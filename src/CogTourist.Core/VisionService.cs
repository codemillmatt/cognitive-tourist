using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using System.IO;
using System.Text;
using System.Linq;
using System;

namespace CogTourist.Core
{
    public class VisionService
    {
        static readonly string apiKey = "fb6e847547c84b2baf20dc43ae0681f8";
        static readonly string url = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0";

        static readonly string could_not_analyze = "Couldn't analyze";

        readonly VisionServiceClient client;

        public VisionService()
        {
            client = new VisionServiceClient(apiKey, url);
        }

        public async Task<string> DescribePhoto(Stream photo)
        {
            try
            {
                var descReturn = await client.DescribeAsync(photo);

                return descReturn?.Description?.Captions.FirstOrDefault()?.Text ?? could_not_analyze;
            }
            catch(Exception ex)
            {
                return could_not_analyze;
            }
        }

        public async Task<string> OCRPhoto(Stream photo)
        {
            try
            {
                var theFullReturn = new StringBuilder();

                var textReturn = await client.RecognizeTextAsync(photo, "fr");

                foreach (var item in textReturn.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            theFullReturn.Append($"{word.Text} ");
                        }
                        theFullReturn.AppendLine();
                    }
                }

                if (theFullReturn.Length == 0)
                    theFullReturn.Append(could_not_analyze);

                return theFullReturn.ToString();
            }
            catch
            {
                return could_not_analyze;
            }
        }
    }
}
