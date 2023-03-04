using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System;
using System.Reflection.Metadata;
using WebAPI.NVMINH.API.Entities;

namespace WebAPI.NVMINH.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult GetAllDepartment()
        {

        
             try
             {
                /// Khởi tạo kêt nối tới DB
                string connectionString = "Server=localhost;Port=3306;Database=web.nvminh.database;Uid=root;Pwd=18091998";
                var mySqlConnection = new MySqlConnection(connectionString);

                  /// Chuẩn bị câu lệnh truy vấn
                string getAllDepartmentsCommand = "SELECT * FROM department;";

                /// Thực hiện gọi vào DB để chạy câu lệnh truy vấn trên
                var departments = mySqlConnection.Query<Department>(getAllDepartmentsCommand);

                /// Xử lý dữ liệu trả về
                if (departments != null)
                {
                return StatusCode(StatusCodes.Status200OK, departments);
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
        /// API thêm mới phòng ban
        /// </summary>
        /// <returns>Danh sách cả vị trí</returns>

        [HttpPost]

        public IActionResult InsertDepartments([FromBody] Department department)
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=web.nvminh.database;Uid=root;Pwd=18091998";
                var mySqlConnection = new MySqlConnection(connectionString);

                // chuẩn bị câu lệnh MySQL

                string insertDepartmentsCommand = "INSERT INTO department (DepartmentID, DepartmentCode, DepartmentName, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) VALUES (@DepartmentID, @DepartmentCode, @DepartmentName, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate);";

                // chuẩn bị thông số đầu vào 
                var departmentID = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentID", departmentID);
                parameters.Add("@DepartmentCode", department.DepartmentCode);
                parameters.Add("@DepartmentName", department.DepartmentName);
                parameters.Add("@CreatedBy", department.CreatedBy);
                parameters.Add("@CreatedDate", department.CreatedDate);
                parameters.Add("@ModifiedBy", department.ModifiedBy);
                parameters.Add("@ModifiedDate", department.ModifiedDate);

                // thực hiện gọi vào DB để chạy câu lệnh truy vấn 

                int numberOfAffectedRows = mySqlConnection.Execute(insertDepartmentsCommand, parameters);

                // xử lý kết quả trả về từ DB

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, departmentID);

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

        /// <summary>
/// API sửa phòng ban
/// </summary>
/// <param name="department">phòng ban cần sửa</param>
/// <param name="departmentID">ID của phòng ban cần sửa</param>
/// <returns> phòng ban cần sửa</returns>
        [HttpPut]
        [Route("{departmentID}")]
        public IActionResult UpdateDepartment([FromBody] Department department, [FromRoute] Guid departmentID)
        {
            try
            {
                // khởi tạo kết nối tới mySQL
                string connectionString = "Server=localhost1;Port=3306;Database=daotao.ai.2023.nvminh;Uid=root;Pwd=18091998";
                var mySqlConnection = new MySqlConnection(connectionString);

                // chuẩn bị câu lệnh kết nối 

                string updateDepartment = "UPDATE department d SET DepartmentID =" +
                " @DepartmentID, DepartmentCode = @DepartmentCode, DepartmentName =" +
                " @DepartmentCode, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, " +
                "ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate " +
                "WHERE DepartmentID = @DepartmentID;";

                // chuẩn bị tham số đầu vào cho câu lệnh

                var departmentIDs = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentID", departmentIDs);
                parameters.Add("@DepartmentCode", department.DepartmentCode);
                parameters.Add("@DepartmentName", department.DepartmentName);
                parameters.Add("@CreatedBy", department.CreatedBy);
                parameters.Add("@CreatedDate", department.CreatedDate);
                parameters.Add("@ModifiedBy", department.ModifiedBy);
                parameters.Add("@ModifiedDate", department.ModifiedDate);

                // thực hiện gọi vào DB 

                var Departments = mySqlConnection.Query<Department>(updateDepartment, parameters);

                //xử lý dữ liệu trả về

                if (Departments != null)
                {
                    return StatusCode(StatusCodes.Status200OK, departmentIDs);
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

        [HttpDelete]
        [Route("{departmentID}")]
        public IActionResult UpdateDpartment([FromRoute] Guid departmentID)
        {
            try
            {
                // khởi tạo kết nối tới mySQL
                string connectionString = "Server=localhost1;Port=3306;Database=daotao.ai.2023.nvminh;Uid=root;Pwd=18091998";
                var mySqlConnection = new MySqlConnection(connectionString);

                // chuẩn bị câu lệnh kết nối 
                string deleteDepartmentCommand = "DELETE FROM department WHERE DepartmentID = @DepartmentID;";

                // chuẩn bị tham số đầu vào

                var departmentsID = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentID", departmentsID);

                // gọi vào DB thực hiện câu lệnh truy vấn
                var Departments = mySqlConnection.Query<Department>(deleteDepartmentCommand, parameters);
                
                // xử lý dữ liệu trả về
                if(departmentsID != null)
                {
                    return StatusCode(StatusCodes.Status200OK, departmentsID);

                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "E003");
                }




            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "E003");
            }
        }
    }
}
