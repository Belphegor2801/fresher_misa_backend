namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Lỗi dữ liệu không hợp lệ
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class InvalidDataException : Exception
    {
        #region Properties
        /// <summary>
        /// Mã lỗi, mặc định là 400
        /// </summary>
        public int ErrorCode { get; set; } = 400;
        #endregion

        #region Constructor

        public InvalidDataException() { }

        public InvalidDataException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        public InvalidDataException(string message) : base(message){}

        public InvalidDataException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
