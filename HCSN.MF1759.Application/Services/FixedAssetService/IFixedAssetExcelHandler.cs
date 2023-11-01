using HCSN.MF1759.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Interface xử lý nghiệp vụ với tài sản
    /// </summary>
    /// Created by: nxhinh (04/10/2023)
    public interface IFixedAssetExcelHandler : IBaseExcelHandler<FixedAssetDto, FixedAssetCreateDto>
    {
        /// <summary>
        /// Chuyển dữ liệu file excel thành list CreateDto
        /// </summary>
        /// <param name="fileBytes">File excel</param>
        /// <returns></returns>
        /// Created by: nxhinh (04/10/2023)
        public Task<IEnumerable<FixedAssetCreateDto>> GetDataFromExcel(byte[] fileBytes);
    }
}
