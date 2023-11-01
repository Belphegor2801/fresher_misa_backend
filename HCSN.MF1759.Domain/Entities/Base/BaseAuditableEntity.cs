namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Base entity cho các entity có audit
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public abstract class BaseAuditableEntity
    {
        /// <summary>
        /// Người khởi tạo
        /// </summary>
        public string? created_by { get; set; }

        /// <summary>
        /// Ngày khởi tạo
        /// </summary>
        public DateTime? created_date { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string? modified_by { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? modified_date { get; set; } 
    }
}
