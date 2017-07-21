using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace CogTourist.Core
{
    public class EmotionService
    {
        const string recognize_endpoint = "recognize";

        public async Task<List<Emotion>> RecognizeEmotions(Stream image)
        {
            try
            {
                UriBuilder builder = new UriBuilder(CognitiveServiceLogin.EmotionUrl);

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage())
                {
                    image.Position = 0;
                    var ms = new MemoryStream();
                    await image.CopyToAsync(ms);

                    var ba = ms.ToArray();

                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", CognitiveServiceLogin.EmotionAPIKey);

                    //var ue = new UploadEmotion { url = "https://codemilltech.com/content/images/2017/03/contact-2.jpg" };
                    //var ues = JsonConvert.SerializeObject(ue);

                    //using (var strContent = new StringContent(ues))
                    //{
                    //    strContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    //    var why = await client.PostAsync(CognitiveServiceLogin.EmotionUrl, strContent);


                    //    var yo = await why.Content.ReadAsStringAsync();

                    //    var yoyoyo = JsonConvert.DeserializeObject<List<Emotion>>(yo);
                    //}

                    image.Position = 0;
                    ms.Position = 0;
                    using (var sContent = new StreamContent(ms))
                    {
                        sContent.Headers.Remove("Content-Type");
                        sContent.Headers.TryAddWithoutValidation("Content-Type", "application/octet-stream");
                        //sContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        //sContent.Headers.ContentLength = image.Length;

                        var res = await client.PostAsync(CognitiveServiceLogin.EmotionUrl, sContent);

                        var succ = res.StatusCode;
                    }

                    using (var content = new ByteArrayContent(ba))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        content.Headers.ContentLength = ba.GetLength(0);

                        var res = await client.PostAsync(CognitiveServiceLogin.EmotionUrl, content);

                        var succ = res.IsSuccessStatusCode;
                    }


                    request.Method = HttpMethod.Post;
                    request.RequestUri = builder.Uri;

                    request.Headers.Add("Ocp-Apim-Subscription-Key", CognitiveServiceLogin.EmotionAPIKey);



                    request.Content = new ByteArrayContent(ba);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                    client.Timeout = TimeSpan.FromSeconds(60);

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var rawResult = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<List<Emotion>>(rawResult);
                    }
                    else
                    {
                        var res = await response.Content.ReadAsStringAsync();
                        return null;
                    }

                }
            }
            catch (Exception ex)
            {
                var s = ex.ToString();
                return null;
            }
        }
    }
}

