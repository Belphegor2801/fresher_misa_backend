namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface cho các bảng có key
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public interface IHaskey
    {
        /// <summary>
        /// Trả về key của bàng
        /// </summary>
        /// <returns>Key của bảng</returns>
        /// Author: nxhinh (11/09/2023)  
        Guid GetKey();
    }
}
