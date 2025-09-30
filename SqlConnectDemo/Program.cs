using System;
using Microsoft.Data.SqlClient;

public partial class Program
{
    static void Main()
    {
        var connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=Thang@123;TrustServerCertificate=True;";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                Console.WriteLine("✅ Kết nối thành công!");

                // Ví dụ query
                string sql = "SELECT TOP 5 Id, Name FROM Users";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]} - {reader["Name"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}
