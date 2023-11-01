using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Quản lý nghiệp vụ của loại tài sản
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class FixedAssetCategoryManager : IFixedAssetCategoryManager
    {
        private readonly IFixedAssetCategoryRepository _fixedAssetCategoryRepository;

        public FixedAssetCategoryManager(IFixedAssetCategoryRepository fixedAssetCategoryRepository)
        {
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
        }

        /// <summary>
        /// Kiểm tra mã loại tài sản đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã loại tài sản</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">Lỗi trả về nếu đã tồn tại</exception>
        /// Author: nxhinh (22/09/2023)  
        public async Task CheckExistByCodeAsync(string code)
        {
            var fixedAssetCategory = await _fixedAssetCategoryRepository.FindByCodeAsync(code);

            if (fixedAssetCategory != null)
            {
                throw new ConflictException(ResourceVN.Error_DuplicateFixedAssetCategoryCode);
            }
        }
    }
}
