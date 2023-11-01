using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Interface service của chi tiết chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023)
    public interface ITransferDocumentDetailsService : IBaseService<TransferDocumentDetailsDto, TransferDocumentDetailsCreateDto, TransferDocumentDetailsUpdateDto>
    {

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="transferDocumentDetailsDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)
        Task InsertMultiAsync(IEnumerable<TransferDocumentDetailsCreateDto> transferDocumentDetailsDtosList);

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="transferDocumentDetailsDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)
        Task UpdateMultiAsync(IEnumerable<TransferDocumentDetailsUpdateDto> transferDocumentDetailsUpdateDtosList);

        /// <summary>
        /// Lấy tổng số bản ghi tài sản được điều chuyển trong chứng từ
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023) 
        Task<int> GetTotalRecordsByDocumentId(string document_id, FilterObject filterObject);
    }
}
