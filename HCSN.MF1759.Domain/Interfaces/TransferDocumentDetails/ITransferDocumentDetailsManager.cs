using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface quản lý chi tiết chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023)  
    public interface ITransferDocumentDetailsManager
    {
        /// <summary>
        /// Kiểm tra dữ liệu chi tiết chứng từ đã hợp lệ chưa
        /// </summary>
        /// <param name="transferDocumentDetails">Chi tiết chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        Task ValidateData(TransferDocument transferDocument, TransferDocumentDetails transferDocumentDetails);

        /// <summary>
        /// Validate dữ liệu khi xóa
        /// </summary>
        /// <param name="transferDocument">Chứng từ</param>
        /// <param name="transferDocumentDetails">Chi tiết chứng từ</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Exception lỗi dữ liệu</exception>
        /// Author: nxhinh (30/10/2023)  
        Task ValidateDelete(TransferDocument transferDocument, TransferDocumentDetails transferDocumentDetails);
    }
}
