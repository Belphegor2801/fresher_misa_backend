using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Entity chi tiết chứng từ điều chuyển tài sản
    /// </summary>
    /// Created by: nxhinh (25/10/2023) 
    public class TransferDocumentDetails: IHaskey
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
        [OnlyForGet]
        public string fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [OnlyForGet]
        public string fixed_asset_name { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        [OnlyForGet]
        public decimal? cost { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        [OnlyForGet]
        public decimal? remaining_value { get; set; }

        /// <summary>
        /// Id phòng ban trước điều chuyển
        /// </summary>
        public Guid department_before_id { get; set; }

        /// <summary>
        /// Tên phòng ban trước điều chuyển
        /// </summary>
        public string department_before_name { get; set ; }

        /// <summary>
        /// Id phòng ban sau điều chuyển
        /// </summary>
        public Guid department_after_id { get;set; }

        /// <summary>
        /// Tên phòng ban sau điều chuyển
        /// </summary>
        public string department_after_name { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        public string? reason { get; set; }

        /// <summary>
        /// Lấy id chi tiết chứng từ
        /// </summary>
        /// <returns>id chi tiếts chứng từ</returns>
        /// Author: nxhinh (25/10/2023)  
        public Guid GetKey()
        {
            return document_details_id;
        }
    }
}
