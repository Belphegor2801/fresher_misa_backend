using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Interface service của chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023)
    public interface ITransferDocumentService : IBaseService<TransferDocumentDto, TransferDocumentCreateDto, TransferDocumentUpdateDto>
    {
        /// <summary>
        /// Tạo code mới
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)
        Task<string> NewCodeAsync();

        /// <summary>
        /// Lấy danh sách tài sản được điều chuyển trong chứng từ
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023) 

        Task<IEnumerable<TransferDocumentDetailsDto>> GetDetails(Guid document_id, FilterObject? filterObject);
    }
}
