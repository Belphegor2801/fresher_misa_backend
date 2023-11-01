using Newtonsoft.Json;
using System.Text.Json;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Ngoại lệ trả về ở endpoint
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class EndPointException
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Dev message
        /// </summary>
        public string? DevMessage { get; set; }

        /// <summary>
        /// User message
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// Mã theo dõi
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        public string? MoreInfor { get; set; }

        /// <summary>
        /// Lỗi
        /// </summary>
        public object? Errors { get; set; }
        #endregion

        #region Methods
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion

    }
}
