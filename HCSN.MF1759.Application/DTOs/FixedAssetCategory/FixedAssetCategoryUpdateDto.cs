using System.ComponentModel.DataAnnotations;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Dto cập nhật loại tài sản
    /// </summary>
    /// Author: nxhinh (22/09/2023) 
    public class FixedAssetCategoryUpdateDto
    {
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// Created by: nxhinh (22/09/2023)
        [Required]
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã code loại tài sản 
        /// </summary>
        /// Created by: nxhinh (22/09/2023)
        [Required(ErrorMessage = "Mã loại tài sản không được phép để trống")]
        [MaxLength(50, ErrorMessage = "Mã loại tài sản không được vượt quá 50 ký tự")]
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created by: nxhinh (22/09/2023)
        [Required(ErrorMessage = "Tên loại tài sản không được phép để trống")]
        [MaxLength(255, ErrorMessage = "Tên loại tài sản không được vượt quá 255 ký tự")]
        public string? fixed_asset_category_name { get; set; }
    }
}
