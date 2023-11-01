using System.ComponentModel.DataAnnotations;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Dto tạo mới loại tài sản
    /// </summary>
    /// Created by: nxhinh (22/09/2023)
    public class FixedAssetCategoryCreateDto
    {
        /// <summary>
        /// Mã code loại tài sản 
        /// </summary>
        /// Created by: nxhinh (22/09/2023)
        [Required(ErrorMessage = "Mã loại tài sản không được phép để trống!")]
        [MaxLength(50, ErrorMessage = "Mã loại tài sản không được vượt quá 50 ký tự!")]
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created by: nxhinh (22/09/2023)
        [Required(ErrorMessage = "Tên loại tài sản không được phép để trống!")]
        [MaxLength(255, ErrorMessage = "Tên loại tài sản không được vượt quá 255 ký tự!")]
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public float? depreciation_rate { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? life_time { get; set; }
    }
}
