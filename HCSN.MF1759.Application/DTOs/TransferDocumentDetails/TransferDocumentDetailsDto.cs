using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Dto chi tiết chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023) 
    public class TransferDocumentDetailsDto
    {
        /// <summary>
        /// Id chi tiết chứng từ
        /// </summary>
        public Guid document_details_id { get; set; }

        /// <summary>
        /// Id chứng từ
        /// </summary>
        public Guid document_id { get; set; }

        /// <summary>
        /// Id tài sản
        /// </summary>
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string fixed_asset_name { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal? cost { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public decimal? remaining_value { get; set; }

        /// <summary>
        /// Id phòng ban trước điều chuyển
        /// </summary>
        public Guid department_before_id { get; set; }

        /// <summary>
        /// Tên phòng ban trước điều chuyển
        /// </summary>
        public string department_before_name { get; set; }

        /// <summary>
        /// Id phòng ban sau điều chuyển
        /// </summary>
        public Guid department_after_id { get; set; }

        /// <summary>
        /// Tên phòng ban sau điều chuyển
        /// </summary>
        public string department_after_name { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        public string? reason { get; set; }
    }
}
