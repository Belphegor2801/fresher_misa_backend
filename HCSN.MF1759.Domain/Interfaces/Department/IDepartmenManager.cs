namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface quản lý dữ liệu của phòng ban
    /// </summary>
    /// Author: nxhinh (11/09/2023)   
    public interface IDepartmentManager
    {
        /// <summary>
        /// Kiểm tra mã phòng ban đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã phòng ban</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">Lỗi trả về nếu đã tồn tại</exception>
        /// Author: nxhinh (22/09/2023)  
        Task CheckExistByCodeAsync(string code);
    }
}
