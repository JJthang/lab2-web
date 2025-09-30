using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("connect")]
    public IActionResult TestConnection()
    {
        var connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=Thang@123;TrustServerCertificate=True;";

        using (var conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                return Ok("✅ Kết nối thành công!");
            }
            catch (Exception ex)
            {
                return BadRequest("❌ Lỗi kết nối: " + ex.Message);
            }
        }
    }
}
