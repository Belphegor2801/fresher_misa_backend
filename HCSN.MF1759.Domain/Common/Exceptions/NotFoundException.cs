namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Lỗi không tìm thấy dữ liệu
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class NotFoundException : Exception
    {
        #region Properties
        /// <summary>
        /// Mã lỗi, mặc định là 404
        /// </summary>
        /// Author: nxhinh (11/09/2023)  
        public int ErrorCode { get; set; } = 404;
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo lỗi không tìm thấy dữ liệu
        /// </summary>
        /// Author: nxhinh (11/09/2023)  
        public NotFoundException() { }

        /// <summary>
        /// Khời tạo lỗi không tìm thấy dữ liệu với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo lỗi không tìm thấy dữ liệu với thông điệp
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message) : base(message){
        }

        /// <summary>
        /// Khởi tạo lỗi không tìm thấy dữ liệu với thông điệp và mã lỗi
        /// </summary>
        /// <param name="message">Thông điệp</param>
        /// <param name="errorCode">Mã lỗi</param>
        /// Author: nxhinh (11/09/2023)  
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
