using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface repository của chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023)  
    public interface ITransferDocumentRepository : IBaseRepository<TransferDocument>
    {
        /// <summary>
        /// Lấy chi tiết tài sản điều chuyển trong chứng từ
        /// </summary>
        /// Author: nxhinh (25/10/2023)  
        Task<IEnumerable<TransferDocumentDetails>> GetDetails(Guid document_id, FilterObject? filterObject);

        /// <summary>
        /// Tìm danh sách chứng từ của tài sản
        /// </summary>
        /// <param name="fixed_asset_id">id tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        Task<IEnumerable<TransferDocument>> GetListByFixedAssetId(Guid fixed_asset_id);

        /// <summary>
        /// Tìm danh sách chứng từ của tài sản sau thời gian chỉ định
        /// </summary>
        /// <param name="fixed_asset_id">id tài sản</param>
        /// <param name="date">thời gian</param>
        /// <returns></returns>
        /// Author: nxhinh (08/11/2023)  
        Task<IEnumerable<TransferDocument>> GetListByFixedAssetIdAfterDate(Guid fixed_asset_id, DateTime date);
    }
}
