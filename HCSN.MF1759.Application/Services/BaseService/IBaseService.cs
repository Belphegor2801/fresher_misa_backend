namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Interface base service có các phương thức cơ bản thêm sửa xóa
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// <typeparam name="TEntityUpdateDto"></typeparam>
    /// Author: nxhinh (11/09/2023)  
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi</param>
        /// <returns></returns>
        /// Created by: nxhinh (11/09/2023) 
        Task InsertAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Bản ghi sửa</param>
        /// <returns></returns>
        /// Created by: nxhinh (11/09/2023) 
        Task UpdateAsync(TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Xoá bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Created by: nxhinh (11/09/2023) 
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task DeleteMultiAsync(List<Guid> ids);
    }
}
