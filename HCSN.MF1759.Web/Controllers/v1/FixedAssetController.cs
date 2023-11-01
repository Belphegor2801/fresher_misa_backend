using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using System.Text.Json;
using System.IO;

namespace HCSN.MF1759.Controllers
{
    /// <summary>
    /// Controller cho tài sản
    /// </summary>
    /// Created by: nxhinh (11/09/2023) 
    [Route("api/v1/fixed-assets")]
    [ApiController]
    public class FixedAssetController : BaseController<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        private readonly IFixedAssetService _fixedAssetService;

        public FixedAssetController(IFixedAssetService fixedAssetService) : base(fixedAssetService)
        {
            _fixedAssetService = fixedAssetService;
        }

        /// <summary>
        /// Tạo mã code mới
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpGet("codes/new")]
        public async Task<IActionResult> GetNewCode()
        {
            var result = await _fixedAssetService.NewCodeAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="filterJson">Bộ lọc</param>
        /// <returns></returns>
        /// Created by: nxhinh (04/10/2023)
        [HttpGet("excel")]
        public async Task<IActionResult> ExportToExcel([FromQuery] string? filterJson, string? excelOptionsJson)
        {
            var filterObject = new FilterObject();
            var excelOptions = new ExcelOptions();

            try
            {
                filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);
            }
            catch { }

            excelOptions = JsonSerializer.Deserialize<ExcelOptions>(excelOptionsJson);

            if (filterObject == null || excelOptions == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }


            var fileBytes = await _fixedAssetService.ExportToExcel(filterObject, excelOptions);
            var excelName = excelOptions.FileName;

            var file = File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            return file;
        }
    }
}
