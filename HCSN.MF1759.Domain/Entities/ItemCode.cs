using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Entity sinh mã tự động
    /// </summary>
    /// Created by: nxhinh (30/09/2023)
    public class ItemCode
    {
        /// <summary>
        /// Tiền tố
        /// </summary>
        /// Created by: nxhinh (30/09/2023)
        public string? prefix { get; set; }

        /// <summary>
        /// Hậu tố
        /// </summary>
        /// Created by: nxhinh (30/09/2023)
        public string? suffix { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        /// Created by: nxhinh (30/09/2023)
        public int base_value { get; set; } = 0;

        /// <summary>
        /// Độ dài phần giá trị
        /// </summary>
        /// Created by: nxhinh (30/09/2023)
        public int value_length { get; set; }
    }
}
