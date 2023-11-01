using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using System.Text.Json;

namespace HCSN.MF1759.Controllers
{
    /// <summary>
    /// Controller cho chứng từ
    /// </summary>
    /// Created by: nxhinh (25/10/2023)
    [Route("api/v1/transfer-document")]
    [ApiController]
    public class TransferDocumentController : BaseController<TransferDocumentDto, TransferDocumentCreateDto, TransferDocumentUpdateDto>
    {
        private readonly ITransferDocumentService _transferDocumentService;
        public TransferDocumentController(ITransferDocumentService transferDocumentService) : base(transferDocumentService)
        {
            _transferDocumentService = transferDocumentService;
        }

        /// <summary>
        /// Lấy danh sách tài sản được điều chuyển trong chứng từ
        /// </summary>
        /// <param name="document_id">Id của chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        [HttpGet("details")]
        public async Task<IActionResult> GetDetails([FromQuery] Guid document_id, string? filterJson)
        {
            var filterObject = new FilterObject();
            try
            {
                filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);
            }
            catch { }

            var result = await _transferDocumentService.GetDetails(document_id, filterObject);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Tạo mã code mới
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        [HttpGet("codes/new")]
        public async Task<IActionResult> GetNewCode()
        {
            var result = await _transferDocumentService.NewCodeAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

    }
}
