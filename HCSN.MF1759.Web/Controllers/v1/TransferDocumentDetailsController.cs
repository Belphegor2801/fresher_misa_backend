using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759.Application;
using HCSN.MF1759.Domain;
using System.Text.Json;

namespace HCSN.MF1759.Controllers
{
    /// <summary>
    /// Controller cho chi tiết chứng từ
    /// </summary>
    /// Created by: nxhinh (25/10/2023)
    [Route("api/v1/transfer-document-details")]
    [ApiController]
    public class TransferDocumentDetailsController : BaseController<TransferDocumentDetailsDto, TransferDocumentDetailsCreateDto, TransferDocumentDetailsUpdateDto>
    {
        private readonly ITransferDocumentDetailsService _transferDocumentDetailsService;
        public TransferDocumentDetailsController(ITransferDocumentDetailsService transferDocumentDetailsService) : base(transferDocumentDetailsService)
        {
            _transferDocumentDetailsService = transferDocumentDetailsService;
        }

        /// <summary>
        /// Lấy tổng số tài sản được điều chuyển trong chứng từ
        /// </summary>l
        /// <param name="document_id">Id của chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        [HttpGet("totalRecordsByDocumentId")]
        public async Task<IActionResult> GetTotalRecordsByDocumentId([FromQuery] string document_id, string? filterJson)
        {
            var filterObject = new FilterObject();
            try
            {
                filterObject = JsonSerializer.Deserialize<FilterObject>(filterJson);
            }
            catch { }

            var result = await _transferDocumentDetailsService.GetTotalRecordsByDocumentId(document_id, filterObject);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
