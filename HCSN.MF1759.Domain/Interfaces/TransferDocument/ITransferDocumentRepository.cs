using System;
using System.Collections.Generic;
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
    }
}
