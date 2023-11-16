using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using System.Text.Json;

namespace HCSN.MF1759.Controllers
{
    /// <summary>
    /// Controller cho thông tin giao nhận
    /// </summary>
    /// Created by: nxhinh (25/10/2023)
    [Route("api/v1/recipient")]
    [ApiController]
    public class RecipientController: BaseController<RecipientDto, RecipientCreateDto, RecipientUpdateDto>
    {
        private readonly IRecipientService _recipientService;
        public RecipientController(IRecipientService recipientService) : base(recipientService)
        {
            _recipientService = recipientService;
        }

        /// <summary>
        /// Lấy danh sách chứng từ của tài sản
        /// </summary>
        /// <param name="fixed_asset_id">id tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (08/11/2023)  
        [HttpGet("getByDocumentId")]
        public async Task<IActionResult> GetListByDocumentId([FromQuery] Guid document_id)
        {
            var result = await _recipientService.GetListByDocumentId(document_id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy thông tin giao nhận được thêm vào lần cuối
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (14/11/2023)  
        [HttpGet("getLast")]
        public async Task<IActionResult> GetLast()
        {
            var result = await _recipientService.GetLast();
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
