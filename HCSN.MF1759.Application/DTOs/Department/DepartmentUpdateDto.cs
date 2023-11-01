using System.ComponentModel.DataAnnotations;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Dto cập nhật phòng ban
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class DepartmentUpdateDto
    {
        /// <summary>
        /// Id phòng ban
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        [Required]
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã code phòng ban 
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        [Required(ErrorMessage = "Mã phòng ban không được phép để trống")]
        [MaxLength(50, ErrorMessage = "Mã phòng ban không được vượt quá 50 ký tự")]
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        [Required(ErrorMessage = "Tên phòng ban không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Tên phòng ban không được vượt quá 255 ký tự")]
        public string? department_name { get; set; }
    }
}
