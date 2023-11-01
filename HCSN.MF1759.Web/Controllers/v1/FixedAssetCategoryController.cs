using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759.Application;

namespace HCSN.MF1759.Controllers
{
    /// <summary>
    /// Controller cho loại tài sản
    /// </summary>
    /// Created by: nxhinh (22/09/2023)
    [Route("api/v1/fixed-asset-categories")]
    [ApiController]
    public class FixedAssetCategoryController : BaseController<FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>
    {
        private readonly IFixedAssetCategoryService _fixedAssetCategoryService;
        public FixedAssetCategoryController(IFixedAssetCategoryService fixedAssetCategoryService) : base(fixedAssetCategoryService)
        {
            _fixedAssetCategoryService = fixedAssetCategoryService;
        }
    }
}
