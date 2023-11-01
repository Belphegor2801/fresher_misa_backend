using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Service của loại tài sản
    /// </summary>
    /// Created by: nxhinh (22/09/2023)
    public class FixedAssetCategoryService : BaseService<FixedAssetCategory, FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>, IFixedAssetCategoryService
    {
        #region Constructor
        public FixedAssetCategoryService(IFixedAssetCategoryRepository fixedAssetCategoryRepository, IMapper mapper, IFixedAssetCategoryManager fixedAssetCategoryManager, IUnitOfWork unitOfWork) : base(fixedAssetCategoryRepository, mapper, unitOfWork)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Chuyển từ FixedAssetCategoryCreateDto sang FixedAssetCategory
        /// </summary>
        /// <param name="entityCreateDto">fixedAssetCategory cần chuyển</param>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023) 
        public override Task<FixedAssetCategory> MapCreateDtoToEntity(FixedAssetCategoryCreateDto fixedAssetCategoryCreateDto)
        {
            var fixedAssetCategory = _mapper.Map<FixedAssetCategory>(fixedAssetCategoryCreateDto);

            fixedAssetCategory.fixed_asset_category_id = Guid.NewGuid();

            return Task.FromResult(fixedAssetCategory);
        }

        /// <summary>
        /// Chuyển từ FixedAssetCategoryUpdateDto sang FixedAssetCategory
        /// </summary>
        /// <param name="entityUpdateDto">fixedAssetCategory cần chuyển</param>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023) 
        public override Task<FixedAssetCategory> MapUpdateDtoToEntity(FixedAssetCategoryUpdateDto fixedAssetCategoryUpdateDto)
        {
            var fixedAssetCategory = _mapper.Map<FixedAssetCategory>(fixedAssetCategoryUpdateDto);

            return Task.FromResult(fixedAssetCategory);
        }
        #endregion
    }
}
