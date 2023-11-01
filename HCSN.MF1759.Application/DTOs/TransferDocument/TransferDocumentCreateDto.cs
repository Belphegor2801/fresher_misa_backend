using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Dto thêm chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023) 
    public class TransferDocumentCreateDto
    {
        /// <summary>
        /// Mã chứng từ
        /// </summary>
        [Required(ErrorMessage = "Mã chứng từ không được phép để trống.")]
        [MaxLength(100, ErrorMessage = "Mã chứng từ không được vượt quá 100 ký tự.")]
        public string document_code { get; set; }

        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        [Required(ErrorMessage = "Ngày điều chuyển không được phép để trống.")]
        public DateTime? transfer_date { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        [Required(ErrorMessage = "Ngày chứng từ không được phép để trống.")]
        public DateTime? document_date { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal? cost { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public decimal? remaining_value { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi
        /// </summary>
        public int? tracked_year { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string note { get; set; }


        /// <summary>
        /// Danh sách tài sản điều chuyển
        /// </summary>
        public IEnumerable<TransferDocumentDetailsUpdateDto> fixed_asset_list { get; set; }
    }
}
