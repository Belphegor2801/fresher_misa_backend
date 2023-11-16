using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// CreateDto thông tin giao nhận
    /// </summary>
    /// Created by: nxhinh (25/10/2023) 
    public class RecipientCreateDto
    {
        // <summary>
        /// Thứ tự trong chứng từ
        /// </summary>
        public int recipient_index { get; set; }

        // <summary>
        /// Tên người giao nhận
        /// </summary>
        public String recipient_name { get; set; }

        // <summary>
        /// Tên phòng ban
        /// </summary>
        public String department { get; set; }

        // <summary>
        /// Chức vụ
        /// </summary>
        public String department_position { get; set; }

        // <summary>
        /// Id chứng từ
        /// </summary>
        public Guid document_id { get; set; }
    }
}
