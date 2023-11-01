using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface base xử lý file excel
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// Created by: nxhinh (04/10/2023)
    public interface IBaseExcelHandler<TEntityDto, TEntityCreateDto>
    {
        /// <summary>
        /// Export dữ liệu ra file excel 
        /// </summary>
        /// <param name="entitiyDtos">Danh sách dữ liệu</param>
        /// <returns></returns>
        /// Created by: nxhinh (04/10/2023)
        Task<byte[]> ExportToExcel(IEnumerable<TEntityDto> entitiyDtos, ExcelOptions excelOptions);

        /// <summary>
        /// Chuyển file excel thành dữ liệu
        /// </summary>
        /// <param name="fileBytes">File excel</param>
        /// <returns></returns>
        /// Created by: nxhinh (04/10/2023)
        Task<IEnumerable<TEntityCreateDto>> ImportFromExcel(byte[] fileBytes);
    }
}
