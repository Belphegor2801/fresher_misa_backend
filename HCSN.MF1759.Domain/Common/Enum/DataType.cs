using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Kiểu dữ liệu
    /// </summary>
    /// Created by: nxhinh (04/10/2023) 
    public enum DataType
    {
        /// <summary>
        /// Kiểu dữ liệu chuỗi
        /// </summary>
        Text,
        /// <summary>
        /// Kiểu dữ liệu số
        /// </summary>
        Number,
        /// <summary>
        /// Kiểu dữ liệu ngày tháng
        /// </summary>
        Date,
        /// <summary>
        /// Kiểu dữ liệu phần trăm
        /// </summary>
        Percentage,
    }
}
