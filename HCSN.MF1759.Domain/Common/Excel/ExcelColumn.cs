using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Các cột trong file excel
    /// </summary>
    /// Created by: nxhinh (04/10/2023)
    public class ExcelColumn
    {
        /// <summary>
        /// Tiêu đề cột
        /// </summary>
        [Required]
        public string? ColumnName { get; set; }

        /// <summary>
        /// Tên trường chứa dữ liệu
        /// </summary>
        [Required]
        public string? FieldName { get; set; }

        /// <summary>
        /// Độ rộng cột
        /// </summary>
        public double? ColumnWidth { get; set; }

        /// <summary>
        /// Kiểu dữ liệu
        /// </summary>
        public DataType DataType { get; set; } = DataType.Text;

    }
}
