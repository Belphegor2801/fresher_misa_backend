namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface quản lý dữ liệu loại tài sản
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public interface IFixedAssetCategoryManager
    {
        /// <summary>
        /// Kiểm tra mã loại tài sản đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã loại tài sản</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">Lỗi trả về nếu đã tồn tại</exception>
        /// Author: nxhinh (22/09/2023)  
        Task CheckExistByCodeAsync(string code);
    }
}
