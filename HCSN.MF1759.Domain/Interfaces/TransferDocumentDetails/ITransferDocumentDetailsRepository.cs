using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface repository của chi tiết chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023)  
    public interface ITransferDocumentDetailsRepository: IBaseRepository<TransferDocumentDetails>
    {
        /// <summary>
        /// Lấy tổng số bản ghi theo Id chứng từ
        /// </summary>
        /// <param name="document_id">id chứng từ</param>
        /// <param name="filterObject">filter</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        Task<int> GetTotalRecordsByDocumentId(string document_id, FilterObject filterObject);

        /// <summary>
        /// Tìm danh sách điều chuyển của tài sản
        /// </summary>
        /// <param name="fixed_asset_id">id tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        Task<IEnumerable<TransferDocumentDetails>> GetListByFixedAssetId(Guid fixed_asset_id);

        /// <summary>
        /// Thêm nhiều
        /// </summary>
        /// <param name="entities">Danh sách entity</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        //Task InsertManyAsync(List<TransferDocumentDetails> entities);

        /// <summary>
        /// Cập nhật nhiều
        /// </summary>
        /// <param name="entities">Danh sách entity</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        //Task UpdateManyAsync(List<TransferDocumentDetails> entities);


    }
}
