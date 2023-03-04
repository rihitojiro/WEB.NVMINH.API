namespace WebAPI.NVMINH.API.Entities
{
    public class Position
    {
        /// <summary>
        /// ID vị trí
        /// </summary>
        public Guid PositionID { get; set; }
        /// <summary>
        /// tên vị trí
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// mã vị trí
        /// </summary>
        public string PositionCode { get; set; }

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

