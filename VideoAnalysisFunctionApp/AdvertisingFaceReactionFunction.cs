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
    public static class AdvertisingFaceReactionFunction
    {

        [FunctionName("AdvertisingFaceReactionFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var str = "YourConnectionString";
            using (var conn = new SqlConnection(str))
            {
                conn.Open();
                var text = @"INSERT INTO dbo.AdvertisingFaceReaction (Advertising, FaceID, Gender, Age, Emotion, Glasses, Beard, Bald, HairColor) 
                     VALUES (@Source, @FaceID, @Gender, @Age, @Emotion, @Glasses, @Beard, @Bald, @HairColor)";

                using (var cmd = new SqlCommand(text, conn))
                {
                    cmd.Parameters.AddWithValue("@Source", data.Source.ToString());
                    cmd.Parameters.AddWithValue("@FaceID", data.FaceID.ToString());
                    cmd.Parameters.AddWithValue("@Gender", data.Gender.ToString());
                    cmd.Parameters.AddWithValue("@Age", data.Age.ToString());
                    cmd.Parameters.AddWithValue("@Emotion", data.Emotion.ToString());
                    cmd.Parameters.AddWithValue("@Glasses", data.Glasses.ToString());
                    cmd.Parameters.AddWithValue("@Beard", double.Parse(data.Beard.ToString()));
                    cmd.Parameters.AddWithValue("@Bald", double.Parse(data.Bald.ToString()));
                    cmd.Parameters.AddWithValue("@HairColor", data.HairColor.ToString());

                    var rows = await cmd.ExecuteNonQueryAsync();

                    return new OkObjectResult($"{rows} rows were inserted");
                }
            }
        }
    }
}
