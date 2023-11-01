using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Interface service của tài sản
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public interface IFixedAssetService : IBaseService<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {

        /// <summary>
        /// Thêm nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task InsertMultiAsync(IEnumerable<FixedAssetCreateDto> fixedAssetDtosList);

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task UpdateMultiAsync(IEnumerable<FixedAssetUpdateDto> fixedAssetUpdateDtosList);


        /// <summary>
        /// Tạo code mới
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task<string> NewCodeAsync();

        /// <summary>
        /// Xuất dữ liệu ra excel
        /// </summary>
        /// <returns></returns>
        /// Created by: nxhinh (04/10/2023)
        Task<byte[]> ExportToExcel(FilterObject filterObject, ExcelOptions excelOptions);
    }
}
