using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface quản lý chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023)  
    public interface ITransferDocumentManager
    {
        /// <summary>
        /// Kiểm tra dữ liệu chứng từ đã hợp lệ chưa
        /// </summary>
        /// <param name="transferDocument">Chi tiết chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        Task ValidateData(TransferDocument transferDocument);

        /// <summary>
        /// Kiểm tra mã chứng từ mới đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã chứng từ</param>
        /// <param name="transferDocumentId">Id chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        Task CheckExistAsync(string code, Guid? transferDocumentId);
    }
}
