using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Interface service của thông tin giao nhận
    /// </summary>
    /// Author: nxhinh (25/10/2023)
    public interface IRecipientService : IBaseService<RecipientDto, RecipientCreateDto, RecipientUpdateDto>
    {
        /// <summary>
        /// Tìm danh sách thông tin giao nhận theo id chứng từ
        /// </summary>
        /// <param name="document_id">id tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (08/11/2023)  
        Task<IEnumerable<RecipientDto>> GetListByDocumentId(Guid document_id);

        /// <summary>
        /// Lấy thông tin giao nhận được thêm vào lần cuối
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (14/11/2023)  
        Task<IEnumerable<RecipientDto>> GetLast();
    }
}
