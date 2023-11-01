namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface base cho repository chỉ đọc
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Author: nxhinh (11/09/2023)  
    public interface IBaseReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả entity
        /// </summary>
        /// <returns>Danh sách entity</returns>
        /// Author: nxhinh (11/09/2023)  
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy entity theo id
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>Entity theo id</returns>
        /// Author: nxhinh (11/09/2023)  
        Task<TEntity> GetAsync(Guid id);


        /// <summary>
        /// Lấy bản ghi theo code
        /// </summary>
        /// <param name="code">nã bản ghi</param>
        /// <returns>Bản ghi theo code</returns>
        /// <exception cref="NotFoundException">Không tìm thấy trả về NotFoundException</exception>
        /// Author: nxhinh (11/09/2023)  
        Task<TEntity> GetByCodeAsync(string code);


        /// <summary>
        /// Lấy danh sách entity
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>Entity theo id</returns>
        /// Author: nxhinh (11/09/2023)  
        Task<IEnumerable<TEntity>> GetListAsync(FilterObject filterObject);

        /// <summary>
        /// Tìm entity theo id
        /// </summary>
        /// <param name="id">Id của entity</param>
        /// <returns>Entity theo id hoặc null</returns>
        /// Author: nxhinh (11/09/2023)  
        /// 
        Task<TEntity?> FindAsync(Guid id);

        /// <summary>
        /// Lấy danh sách bản ghi theo id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task<IEnumerable<TEntity>> GetListByIdsAsync(List<Guid> ids);

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task<int> GetTotalRecordsAsync(FilterObject filterObject);

        /// <summary>
        /// Số bản ghi có mã hoặc id tương ứng
        /// </summary>
        /// <param name="code">Mã định danh</param>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Số bản ghi</returns>
        /// Author: nxhinh (11/09/2023)  
        public Task<int> CountByCodeOrId(string code, Guid? id);
        
        /// <summary>
        /// Lấy toàn bộ mã
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023) 
        public Task<IEnumerable<string>> GetAllCodeAsync();

        /// <summary>
        /// Tìm bản ghi theo mã
        /// </summary>
        /// <param name="code">Mã bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023) 
        public Task<TEntity?> FindByCodeAsync(string code);
    }
}
