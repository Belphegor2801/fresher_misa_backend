using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Filter
    /// </summary>
    /// Author: nxhinh (13/09/2023)
    public class FilterObject
    {
        // Số bản ghi một trang
        public int Limit { get; set; } = 20;
        // Số bản ghi bỏ qua
        public int Offset { get; set; } = 0;
        // Cột lấy dữ liệu
        public string? Columns { get; set; } = null;
        // Sắp xếp theo trường
        public string? SortField { get; set; }
        // Kiểu sắp xếp ASC - DESC
        public string? SortType { get; set; } = "DESC";
        // Điều kiện lọc
        public List<FilterItem>? Filter { get; set; }
        // Tìm kiếm 
        public List<FilterItem>? Search { get; set; }
    }
}
