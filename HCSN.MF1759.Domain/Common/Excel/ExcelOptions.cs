using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Các thuộc tính của file excel
    /// </summary>
    /// Created by: nxhinh (04/10/2023)
    public class ExcelOptions
    {
        /// <summary>
        /// Tên sheet
        /// </summary>
        public string? SheetName { get; set; } = "Sheet1";

        /// <summary>
        /// Tên file
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Tiêu đề cột
        /// </summary>
        public List<ExcelColumn>? Columns { get; set; } = new List<ExcelColumn>();

    }
}
