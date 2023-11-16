using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Quản lý nghiệp vụ tài sản
    /// </summary>
    /// Author: nxhinh (11/09/2023)   
    public class FixedAssetManager : IFixedAssetManager
    {
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly ITransferDocumentRepository _transferdocumentRepository;

        public FixedAssetManager(IFixedAssetRepository fixedAssetRepository, ITransferDocumentRepository transferdocumentRepository)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _transferdocumentRepository = transferdocumentRepository;
        }


        /// <summary>
        /// Validate dữ liệu tài sản
        /// </summary>
        /// <param name="fixedAsset">Tài sản</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Exception lỗi dữ liệu</exception>
        /// Author: nxhinh (11/09/2023)  
        public Task ValidateData(FixedAsset fixedAsset)
        {
            if (fixedAsset.quantity <= 0)
            {
                throw new InvalidDataException(ResourceVN.Error_PositiveQuantity);
            }

            if (fixedAsset.start_using_date?.Date < fixedAsset.purchase_date?.Date)
            {
                throw new InvalidDataException(ResourceVN.Error_PurchaseDateMustLessThenStartUsingDate);
            }

            var depreciationRateOdd = fixedAsset.depreciation_rate - (1.0 / fixedAsset.life_time) * 100;

            if (Math.Abs(depreciationRateOdd ?? 0) > 0.1)
            {
                throw new InvalidDataException(ResourceVN.Error_DepreciationRateError);
            }
            var depreciationValueYearOdd = fixedAsset.depreciation_value_year - fixedAsset.cost * (decimal)(fixedAsset.depreciation_rate ?? 0) / 100;

            if (Math.Abs(depreciationValueYearOdd ?? 0) > 1000)
            {
                throw new InvalidDataException(ResourceVN.Error_DepreciationValueYearError);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Validate dữ liệu tài sản khi xóa
        /// </summary>
        /// <param name="fixedAsset">Tài sản</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">Exception lỗi dữ liệu</exception>
        /// Author: nxhinh (11/11/2023) 
        public async Task ValidateDelete(Guid fixed_asset_id)
        {
            var documentByFixedAssetId = await _transferdocumentRepository.GetListByFixedAssetId(fixed_asset_id);


            if (documentByFixedAssetId != null)
            {
                var lastDocument = documentByFixedAssetId.First();
                throw new InvalidDataException(ResourceVN.Error_FixedAssetDelete.Replace("{code}", lastDocument.document_code).Replace("{date}", lastDocument.transfer_date.ToString("dd/MM/yyyy")));
            }
        }


        /// <summary>
        /// Kiểm tra mã tài sản mới đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023)  
        public async Task CheckExistAsync(string code, Guid? fixedAssetId)
        {
            // Đếm số bản ghi có id và code trùng với dữ liệu truyền vào
            var count = await _fixedAssetRepository.CountByCodeOrId(code, fixedAssetId != null ? fixedAssetId: Guid.Empty);

            // Trả về 1 bản ghi có id tương ứng, trả nếu code đã có trong db và khác code cũ sẽ trả về thêm bản ghi, khi đó thông báo lỗi
            if ((fixedAssetId == null & count > 0) || (fixedAssetId != null & count > 1))
            {
                throw new ConflictException(ResourceVN.Error_DuplicateFixedAssetCode.Replace("{code}", code));
            }
        }
    }
}
