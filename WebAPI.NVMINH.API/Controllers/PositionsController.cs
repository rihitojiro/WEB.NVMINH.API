using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using WebAPI.NVMINH.API.Entities;

namespace WebAPI.NVMINH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        /// <summary>
        /// API lấy danh sách tất cả thông tin vị trí
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult GetAllPosition()
        {
            try
            {   // khởi tạo kết nối tới MySQL
                string connectionString = "Sever=localhost;Port=3306;Database=web.nvminh.database;Uid=root;Pwd=18091998;";
                var mySqlConnection = new MySqlConnection(connectionString);
                // chuẩn bị câu lệnh kết nối tới MySQL
                string getPossitionsCommand = "SELECT * FROM positions;";


                // chuẩn bị câu lệnh truy vấn 
                var positions = mySqlConnection.Query<Position>(getPossitionsCommand);
                // xử lý kết quả trả về
                if (getPossitionsCommand != null)
                {

                    return StatusCode(StatusCodes.Status200OK, positions);

                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "E001");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "E003");
            }
        }
        /// <summary>
        /// API thêm mới vị trí
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertPositions([FromBody] Position position)
        {
            try
            {
                // khởi tạo kết nối tới MySQL
                string connectionString = "Sever=localhost;Port=3306;Database=web.nvminh.database;Uid=root;Pwd=18091998;";
                var mySqlConnection = new MySqlConnection(connectionString);

                // chuẩn bị câu lệnh INSERT INTO

                string insertPositionsCommand = "INSERT INTO positions (PositionID, PositionCode, PositionName, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES (@PositionID, @PositionCode, @PositionName, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate);";
                // chuẩn bị tham số đầu vào cho câu lệnh 

                var positionID = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("@PositionID", positionID);
                parameters.Add("@PositionCode", position.PositionCode);
                parameters.Add("@PositionName", position.PositionName);
                parameters.Add("@CreatedBy", position.CreatedBy);
                parameters.Add("@CreatedDate", position.CreatedDate);
                parameters.Add("@ModifiedBy", position.ModifiedBy);
                parameters.Add("@ModifiedDate", position.ModifiedDate);

                // gọi vào DB câu lệnh truy vấn 

                int numberAffectedOfRows = mySqlConnection.Execute(insertPositionsCommand, parameters);

                // xử lý kết quả trả về 

                if (numberAffectedOfRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, positionID);

                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "E001");
                }
            }
            catch (MySqlException mySqlException)
            {
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "E002");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "E003");
            }



            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "E003");
            }
        }
    }
}
