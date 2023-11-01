using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    public class ConflictException: Exception
    {
        #region Properties
        /// <summary>
        /// Mã lỗi, mặc định là 409
        /// </summary>
        /// Author: nxhinh (22/09/2023)  
        public int ErrorCode { get; set; } = 409;
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo lỗi không tìm thấy dữ liệu
        /// </summary>
        /// Author: nxhinh (22/09/2023)  
        public ConflictException() { }

        /// <summary>
        /// Khời tạo lỗi không tìm thấy dữ liệu với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo lỗi không tìm thấy dữ liệu với thông điệp
        /// </summary>
        /// <param name="message"></param>
        public ConflictException(string message) : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo lỗi không tìm thấy dữ liệu với thông điệp và mã lỗi
        /// </summary>
        /// <param name="message">Thông điệp</param>
        /// <param name="errorCode">Mã lỗi</param>
        /// Author: nxhinh (22/09/2023)  
        public ConflictException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
