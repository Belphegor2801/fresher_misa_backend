using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Bộ tìm kiếm dữ liệu
    /// </summary>
    /// Author: nxhinh (13/09/2023)
    public class FilterItem
    {
        // Trường lọc
        public string? Field { get; set; }
        // Giá trị lọc
        public string? Value { get; set; }
        // Kiểu lọc: "LIKE", "=", "NOT"
        public string? OperatorType { get; set; }
    }
}
