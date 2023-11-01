using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759.Application;

namespace HCSN.MF1759.Controllers
{
    /// <summary>
    /// Base controller cho các controller có thể thêm, sửa, xoá
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// <typeparam name="TEntityUpdateDto"></typeparam>
    /// Created by: nxhinh (11/09/2023) 
    public abstract class BaseController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyController<TEntityDto>
    {
        private readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        protected BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService) : base(baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// Tạo bản ghi mới
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi cần tạo</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] TEntityCreateDto entityCreateDto)
        {
            await _baseService.InsertAsync(entityCreateDto);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Bản ghi cần sửa</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TEntityUpdateDto entityUpdateDto)
        {
            await _baseService.UpdateAsync(entityUpdateDto);

            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Xoá bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần xoá</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _baseService.DeleteAsync(id);

            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id bản ghi cần xoá</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteMultiAsync([FromBody] List<Guid> ids)
        {
            await _baseService.DeleteMultiAsync(ids);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
