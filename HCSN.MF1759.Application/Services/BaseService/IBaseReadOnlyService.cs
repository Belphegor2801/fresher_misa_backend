using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Interface base service chỉ đọc
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// Created by: nxhinh (11/09/2023) 
    public interface IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: nxhinh (11/09/2023) 
        Task<IEnumerable<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: nxhinh (11/09/2023) 
        Task<TEntityDto> GetAsync(Guid id);

        /// <summary>
        /// Lấy bản ghi theo code
        /// </summary>
        /// <param name="code">code bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task<TEntityDto> GetByCodeAsync(string code);

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: nxhinh (11/09/2023) 
        Task<IEnumerable<TEntityDto>> GetListAsync(FilterObject filterObject);

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: nxhinh (11/09/2023) 
        Task<TEntityDto?> FindAsync(Guid id);

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns>Tổng số bản ghi</returns>
        /// Created by: nxhinh (11/09/2023) 
        Task<int> GetTotalRecordsAsync(FilterObject filterObject);

        /// <summary>
        /// Lấy tất cả mã code
        /// </summary>
        /// <returns>Danh sách mã code</returns>
        /// Created by: nxhinh (11/09/2023) 
        Task<IEnumerable<string>> GetAllCodeAsync();
    }
}
