using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using System.Text.Json;

namespace HCSN.MF1759
{
    /// <summary>
    /// Base controller cho các controller chỉ đọc
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// Author: nxhinh (11/09/2023)  
    public abstract class BaseReadOnlyController<TEntityDto> : ControllerBase
    {
        private readonly IBaseReadOnlyService<TEntityDto> _baseReadOnlyService;

        protected BaseReadOnlyController(IBaseReadOnlyService<TEntityDto> baseReadOnlyService)
        {
            _baseReadOnlyService = baseReadOnlyService;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] string? filterJson)
        {
            var filterObject = new FilterObject();
            try
            {
                filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);
            }
            catch { }
            
            var result = await _baseReadOnlyService.GetListAsync(filterObject);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _baseReadOnlyService.GetAsync(id);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy bản ghi theo code
        /// </summary>
        /// <param name="code">Mã bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpGet("getByCode")]
        public async Task<IActionResult> GetByCodeAsync(string code)
        {
            var result = await _baseReadOnlyService.GetByCodeAsync(code);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpGet("totalRecords")]
        public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] string? filterJson)
        {
            var filterObject = new FilterObject();
            try
            {
                filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);
            }
            catch { }

            var result = await _baseReadOnlyService.GetTotalRecordsAsync(filterObject);

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy tất cả mã code
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (30/09/2023)  
        [HttpGet("codes")]
        public async Task<IActionResult> GetAllCodeAsync()
        {
            var result = await _baseReadOnlyService.GetAllCodeAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        
    }
}
