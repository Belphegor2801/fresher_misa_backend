namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface repository của thông tin giao nhận
    /// </summary>
    /// Author: nxhinh (25/10/2023)  
    public interface IRecipientRepository : IBaseRepository<Recipient>
    {
        /// <summary>
        /// Lấy danh sách thông tin giao nhận trong chứng từ
        /// </summary>
        /// <param name="document_id">id chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        Task<IEnumerable<Recipient>> GetListByDocumentId(Guid document_id);

        /// <summary>
        /// Lấy thông tin giao nhận được thêm vào lần cuối
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (14/11/2023)  
        Task<IEnumerable<Recipient>> GetLast();
    }
}
