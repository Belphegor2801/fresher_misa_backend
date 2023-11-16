using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Service của tài sản
    /// </summary>
    /// Created by: nxhinh (25/10/2023)
    public class TransferDocumentDetailsService : BaseService<TransferDocumentDetails, TransferDocumentDetailsDto, TransferDocumentDetailsCreateDto, TransferDocumentDetailsUpdateDto>, ITransferDocumentDetailsService
    {
        private readonly ITransferDocumentDetailsRepository _transferDocumentDetailsRepository;
        private readonly ITransferDocumentDetailsManager _transferDocumentDetailsManager;
        private readonly IFixedAssetRepository _fixedAssetRepository;

        public TransferDocumentDetailsService(
            ITransferDocumentDetailsRepository transferDocumentDetailsRepository, 
            ITransferDocumentDetailsManager transferDocumentDetailsManager,
            IFixedAssetRepository fixedAssetRepository,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(transferDocumentDetailsRepository, mapper, unitOfWork)
        {
            _transferDocumentDetailsRepository = transferDocumentDetailsRepository;
            _transferDocumentDetailsManager = transferDocumentDetailsManager;

            _fixedAssetRepository = fixedAssetRepository;
        }


        /// <summary>
        /// Lấy tổng số bản ghi tài sản được điều chuyển trong chứng từ
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023) 

        public virtual async Task<int> GetTotalRecordsByDocumentId(string document_id, FilterObject filterObject)
        {
            var result = await _transferDocumentDetailsRepository.GetTotalRecordsByDocumentId(document_id, filterObject);

            return result;
        }


        /// <summary>
        /// Chuyển từ TransferDocumentDetailsCreateDto sang TransferDocumentDetails
        /// </summary>
        /// <param name="transferDocumentDetailsCreateDto">TransferDocumentDetailsCreateDto</param>
        /// <returns>TransferDocumentDetails</returns>
        /// Author: nxhinh (25/10/2023) 
        public async override Task<TransferDocumentDetails> MapCreateDtoToEntity(TransferDocumentDetailsCreateDto transferDocumentDetailsCreateDto)
        {
            var transferDocumentDetails = _mapper.Map<TransferDocumentDetails>(transferDocumentDetailsCreateDto);


            // Cập nhật các trường mặc định
            transferDocumentDetails.document_details_id = Guid.NewGuid();

            var fixedAsset = await _fixedAssetRepository.FindAsync(transferDocumentDetails.fixed_asset_id);

            if (fixedAsset == null)
            {
                throw new NotFoundException("Không tìm thấy tài sản");
            }

            transferDocumentDetails.fixed_asset_code = fixedAsset.fixed_asset_code;
            transferDocumentDetails.fixed_asset_name = fixedAsset.fixed_asset_name;
            transferDocumentDetails.cost = fixedAsset.cost;
            transferDocumentDetails.remaining_value = fixedAsset.remaining_value;

            // Kiểm tra dữ liệu
            ///await _transferDocumentDetailsManager.ValidateData(transferDocumentDetails);

            return transferDocumentDetails;
        }

        /// <summary>
        /// Chuyển từ TransferDocumentDetailsUpdateDto sang TransferDocumentDetails
        /// </summary>
        /// <param name="entityUpdateDto">TransferDocumentDetailsUpdateDto</param>
        /// <returns>TransferDocumentDetails</returns>
        /// Author: nxhinh (25/10/2023) 
        public async override Task<TransferDocumentDetails> MapUpdateDtoToEntity(TransferDocumentDetailsUpdateDto transferDocumentDetailsUpdateDto)
        {
            var transferDocumentDetails = _mapper.Map<TransferDocumentDetails>(transferDocumentDetailsUpdateDto);


            // kiểm tra dữ liệu
            ///await _transferDocumentDetailsManager.ValidateData(transferDocumentDetails);
            return transferDocumentDetails;
        }


        /// <summary>
        /// Tạo mới nhiều bản ghi
        /// </summary>
        /// <param name="transferDocumentDetailsDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023) 
        public async Task InsertMultiAsync(IEnumerable<TransferDocumentDetailsCreateDto> transferDocumentDetailsDtosList)
        {
            
        }

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="transferDocumentDetailsUpdateDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023) 
        public async Task UpdateMultiAsync(IEnumerable<TransferDocumentDetailsUpdateDto> transferDocumentDetailsUpdateDtosList)
        {
         
        }
    }
}
