namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface quản lý dữ liệu tài sản
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public interface IFixedAssetManager
    {
        /// <summary>
        /// Kiểm tra dữ liệu tài sản đã hợp lệ chưa
        /// </summary>
        /// <param name="fixedAsset">Tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        Task ValidateData(FixedAsset fixedAsset);

        /// <summary>
        /// Kiểm tra mã tài sản mới đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023) 
        Task CheckExistAsync(string code, Guid? fixedAssetId);
    }
}
