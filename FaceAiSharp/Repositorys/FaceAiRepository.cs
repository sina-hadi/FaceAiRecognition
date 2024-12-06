using FaceAiSharp.Extensions;
using FaceAiSharpApi.Extentions;
using FaceAiSharpApi.Repositorys.IRepositorys;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace FaceAiSharpApi.Repositorys
{
    public class FaceAiRepository : IFaceAiRepository
    {
        private readonly ConfHelper _confHelper;

        public FaceAiRepository(ConfHelper confHelper)
        {
            _confHelper = confHelper;
        }

        public async Task<AppResponse> FaceAiCompareAsync(float[] embedding, int id)
        {
            var jsonArray = String.Empty;
            using (SqlConnection connection = new SqlConnection(_confHelper.ConnectionString))
            {
                connection.Open();

                string query = "SELECT Embedding FROM Recognition WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            jsonArray = reader["Embedding"].ToString();
                        }
                    }
                }
            }

            float[] floatArray = JsonConvert.DeserializeObject<float[]>(jsonArray);

            var dot = embedding.Dot(floatArray);

            if (dot >= 0.42)
            {
                return AppResponse.CreateSuccess();
            }
            else if (dot > 0.28 && dot < 0.42)
            {
                return AppResponse.CreateFailure("CLOSE TO THE GUY!");
            }
            else
            {
                return AppResponse.CreateFailure("NOT THE GUY!");
            }
        }

        public async Task<AppResponse> FaceAiRegisterAsync(string jsonArray)
        {
            using (SqlConnection connection = new SqlConnection(_confHelper.ConnectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Recognition (Embedding) VALUES (@Embedding)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Embedding", jsonArray);

                    await command.ExecuteNonQueryAsync();
                }
            }
            return AppResponse.CreateSuccess();
        }
    }
}
