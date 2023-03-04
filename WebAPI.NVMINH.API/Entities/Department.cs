namespace WebAPI.NVMINH.API.Entities
{
    public class Department
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid DepartmentID { get; set; }
        /// <summary>
        /// tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// người chỉnh sửa
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// ngày chỉnh sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
