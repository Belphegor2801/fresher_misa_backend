namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Entity của phòng ban
    /// </summary>
    /// Created by: nxhinh (11/09/2023) 
    public class Department : BaseAuditableEntity, IHaskey, IHasCode
    {
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        public Guid department_id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Mã phòng ban 
        /// </summary>
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? department_name { get; set; }


        /// <summary>
        /// Lấy id của phòng ban
        /// </summary>
        /// <returns>id của phòng ban</returns>
        /// Author: nxhinh (11/09/2023)  
        public Guid GetKey()
        {
            return department_id;
        }

        /// <summary>
        /// Trả về mã phòng ban
        /// </summary>
        /// <returns>Mã</returns>
        /// Author: nxhinh (22/09/2023)  
        public string GetCode()
        {
            return department_code ?? "";
        }
    }
}
