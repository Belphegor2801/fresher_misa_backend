namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface base repository thêm, sửa, xoá
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Author: nxhinh (11/09/2023)  
    public interface IBaseRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Tạo entity mới
        /// </summary>
        /// <param name="entity">Entity tạo</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Tạo nhiều entity mới
        /// </summary>
        /// <param name="entities">Danh sách entity</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task InsertMultiAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Sửa 1 entity
        /// </summary>
        /// <param name="entity">Entity sửa</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Sửa nhiều entity
        /// </summary>
        /// <param name="entities">Danh sách entity</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task UpdateMultiAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Xoá 1 entity
        /// </summary>
        /// <param name="entity">Entity xoá</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Xoá nhiều entity
        /// </summary>
        /// <param name="entities">Danh sách entity xoá</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task DeleteMultiAsync(IEnumerable<TEntity> entities);
    }
}
