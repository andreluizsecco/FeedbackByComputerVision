using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsWebApp.Data
{
    public class Repository
    {
        private readonly string _connectionString;

        public Repository(IConfiguration configuration) =>
            _connectionString = configuration.GetConnectionString("SqlConnectionString");

        public async Task<List<string>> GetSources()
        {
            var sources = new List<string>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var text = @"SELECT DISTINCT Source
                            FROM  dbo.FaceReaction
                            ORDER BY Source";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                        sources.Add(reader.GetString(0));
                }
            }
            return sources;
        }

        public async Task<Dictionary<string, decimal>> GetStatistics(string source, string info)
        {
            var result = new Dictionary<string, decimal>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var text = $@"DECLARE @Total DECIMAL = (SELECT CAST(COUNT(*) AS DECIMAL) AS Total FROM dbo.FaceReaction WHERE Source = @Source)

                            SELECT CAST({info} AS VARCHAR) AS {info}, COUNT(*)/@Total*100 AS Quantity
                            FROM  dbo.FaceReaction
                            WHERE Source = @Source
                            GROUP BY {info}
                            ORDER BY {info}";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    cmd.Parameters.AddWithValue("@Source", source);

                    var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                        result.Add(reader.GetString(0), reader.GetDecimal(1));
                }
            }
            return result;
        }

        public async Task ClearAdvertisingStatistics()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var text = @"DELETE FROM  dbo.AdvertisingFaceReaction";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                    await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ClearFaceStatistics()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var text = @"DELETE FROM  dbo.FaceReaction";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                    await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
