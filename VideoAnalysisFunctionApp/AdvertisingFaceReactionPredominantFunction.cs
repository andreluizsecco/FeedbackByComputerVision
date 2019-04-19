using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace VideoAnalysisFunctionApp
{
    public static class AdvertisingFaceReactionPredominantFunction
    {

        [FunctionName("AdvertisingFaceReactionPredominantFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var str = "YourConnectionString";
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var text = @"SELECT TOP(1) Emotion
                    FROM  dbo.AdvertisingFaceReaction
                    WHERE Banner = @Banner AND Glasses = @Glasses AND Gender = @Gender
                    GROUP BY Emotion
                    ORDER BY COUNT(*) DESC, Emotion DESC";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    cmd.Parameters.AddWithValue("@Banner", data.Banner.ToString());
                    cmd.Parameters.AddWithValue("@Gender", data.Gender.ToString());
                    cmd.Parameters.AddWithValue("@Glasses", data.Glasses.ToString());

                    var emotion = await cmd.ExecuteScalarAsync();

                    return new OkObjectResult(emotion);
                }
            }
        }
    }
}
