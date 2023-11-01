using System.ComponentModel.DataAnnotations;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Dto tạo mới phòng ban
    /// </summary>
    /// Created by: nxhinh (11/09/2023) 
    public class DepartmentCreateDto
    {
        /// <summary>
        /// Mã code phòng ban 
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        [Required(ErrorMessage = "Mã phòng ban không được phép để trống!")]
        [MaxLength(50, ErrorMessage = "Mã phòng ban không được vượt quá 50 ký tự!")]
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        [Required(ErrorMessage = "Tên phòng ban không được phép để trống!")]
        [MaxLength(255, ErrorMessage = "Tên phòng ban không được vượt quá 255 ký tự!")]
        public string? department_name { get; set; }
    }
}
