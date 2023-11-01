namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Entity của tài sản
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class FixedAsset : BaseAuditableEntity, IHaskey, IHasCode
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        public Guid fixed_asset_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string? fixed_asset_code { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string? fixed_asset_name { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        public Guid department_id { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string? department_code { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? department_name { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên tài sản 
        /// </summary> 
        public string? fixed_asset_category_name { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTime? purchase_date { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime? start_using_date { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal? cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public float? depreciation_rate { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        public decimal? depreciation_value_year { get; set; }

        /// <summary>
        /// Năm bắt đầu theo dõi tài sản
        /// </summary>
        public int? tracked_year { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? life_time { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        /// Author: nxhinh (11/09/2023)  
        public int? production_year { get; set; }

        /// <summary>
        /// Hao mòn luỹ kế
        /// </summary>
        public decimal? accumulated_depreciation { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public decimal? remaining_value { get; set; }

        /// <summary>
        /// Có đang sử dụng hay không
        /// </summary>
        public bool? active { get; set; }


        /// <summary>
        /// Lấy id của tài sản
        /// </summary>
        /// <returns>Id của tài sản</returns>
        /// Author: nxhinh (11/09/2023)  
        public Guid GetKey()
        {
            return fixed_asset_id;
        }

        /// <summary>
        /// Trả về mã tài sản
        /// </summary>
        /// <returns>Mã</returns>
        /// Author: nxhinh (22/09/2023)  
        public string GetCode()
        {
            return fixed_asset_code ?? "";
        }
    }
}


