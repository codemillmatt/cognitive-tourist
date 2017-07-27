using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using System.IO;
using System.Text;
using System.Linq;
using System;
using Microsoft.ProjectOxford.Vision.Contract;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CogTourist.Core
{
    public class VisionService
    {
        static readonly string could_not_analyze = "Couldn't analyze";
        static readonly string landmark_model = "landmarks";

        readonly VisionServiceClient client;

        public VisionService()
        {
            client = new VisionServiceClient(CognitiveServiceLogin.VisionAPIKey, CognitiveServiceLogin.VisionUrl);
        }

        public async Task<AllLandmarks> DescribePhoto(Stream photo)
        {
            try
            {
                var descReturn = await client.AnalyzeImageInDomainAsync(photo, landmark_model);

                if (!(descReturn.Result is JContainer container))
                    return null ;

                var landmarks = container.ToObject<AllLandmarks>();

                return landmarks;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> OCRPhoto(Stream photo, string originLanguage)
        {
            try
            {
                var theFullReturn = new StringBuilder();

                var textReturn = await client.RecognizeTextAsync(photo, originLanguage);

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
            catch (Exception ex)
            {
                return could_not_analyze;
            }
        }
    }
}
