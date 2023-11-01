using HCSN.MF1759.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Entity của loại tài sản
    /// </summary>
    /// Author: nxhinh (22/09/2023)
    public class FixedAssetCategory : BaseAuditableEntity, IHaskey, IHasCode
    {
        /// <summary>
        /// Id của loại tài sản
        /// </summary>
        public Guid fixed_asset_category_id { get; set; }

        /// <summary>
        /// Mã code loại tài sản
        /// </summary>
        public string? fixed_asset_category_code { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string? fixed_asset_category_name { get; set; }


        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public float? depreciation_rate { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? life_time { get; set; }

        
        /// <summary>
        /// Trả về id
        /// </summary>
        /// <returns>Id</returns>
        /// Author: nxhinh (22/09/2023)  
        public Guid GetKey()
        {
            return fixed_asset_category_id;
        }

        /// <summary>
        /// Trả về mã
        /// </summary>
        /// <returns>Mã</returns>
        /// Author: nxhinh (22/09/2023)  
        public string GetCode()
        {
            return fixed_asset_category_code ?? "";
        }

    }
}