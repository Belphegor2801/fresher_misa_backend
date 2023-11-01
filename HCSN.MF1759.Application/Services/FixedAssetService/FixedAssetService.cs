using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Service của tài sản
    /// </summary>
    /// Created by: nxhinh (11/09/2023) 
    public class FixedAssetService : BaseService<FixedAsset, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>, IFixedAssetService
    {
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly IFixedAssetManager _fixedAssetManager;
        private readonly IFixedAssetExcelHandler _fixedAssetExcelHandler;

        public FixedAssetService(IFixedAssetRepository fixedAssetRepository, IMapper mapper, IFixedAssetManager fixedAssetManager, IUnitOfWork unitOfWork, IFixedAssetExcelHandler fixedAssetExcelHandler) : base(fixedAssetRepository, mapper, unitOfWork)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _fixedAssetManager = fixedAssetManager;
            _fixedAssetExcelHandler = fixedAssetExcelHandler;
        }


        /// <summary>
        /// Tạo code mới
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  

        public virtual async Task<string> NewCodeAsync()
        {
            var codes = await _fixedAssetRepository.GetAllCodeAsync();

            return CodeHandler.NewCode(codes);
        }

        /// <summary>
        /// Xuất dữ liệu ra excel
        /// </summary>
        /// <returns></returns>
        /// Created by: nxhinh (04/10/2023)
        public async Task<byte[]> ExportToExcel(FilterObject filterObject, ExcelOptions excelOptions)
        {
            // Bỏ limit để lấy toàn bộ dữ liệu được lọc
            filterObject.Limit = 10000000;
            filterObject.Offset = 0;

            var fixedAssets = await _fixedAssetRepository.GetListAsync(filterObject);

            var fixedAssetDtos = _mapper.Map<IEnumerable<FixedAssetDto>>(fixedAssets);

            var fileBytes = await _fixedAssetExcelHandler.ExportToExcel(fixedAssetDtos, excelOptions);

            return fileBytes;
        }

        /// <summary>
        /// Chuyển từ FixedAssetCreateDto sang FixedAsset
        /// </summary>
        /// <param name="fixedAssetCreateDto">FixedAssetCreateDto</param>
        /// <returns>FixedAsset</returns>
        /// Author: nxhinh (11/09/2023)  
        public async override Task<FixedAsset> MapCreateDtoToEntity(FixedAssetCreateDto fixedAssetCreateDto)
        {
            var fixedAsset = _mapper.Map<FixedAsset>(fixedAssetCreateDto);

            if (fixedAssetCreateDto.fixed_asset_code != null)
            {
                await _fixedAssetManager.CheckExistAsync(code: fixedAssetCreateDto.fixed_asset_code, null);
            }

            // Cập nhật các trường mặc định
            fixedAsset.fixed_asset_id = Guid.NewGuid();
            fixedAsset.tracked_year = DateTime.Now.Year;
            fixedAsset.active = true;
            fixedAsset.production_year = fixedAsset.start_using_date?.Year;

            // Cập nhật giá trị hao mòn và giá trị còn lại
            var temp = fixedAsset.depreciation_value_year * (DateTime.Now.Year - fixedAsset.production_year);
            if (temp > fixedAsset.cost)
            {
                fixedAsset.accumulated_depreciation = fixedAsset.cost;
            }
            else if (temp < 0)
            {
                fixedAsset.accumulated_depreciation = 0;
            }
            else
            {
                fixedAsset.accumulated_depreciation = temp;
            }
            fixedAsset.remaining_value = fixedAsset.cost - fixedAsset.accumulated_depreciation;

            // Kiểm tra dữ liệu
            await _fixedAssetManager.ValidateData(fixedAsset);
            return fixedAsset;
        }

        /// <summary>
        /// Chuyển từ FixedAssetUpdateDto sang FixedAsset
        /// </summary>
        /// <param name="entityUpdateDto">FixedAssetUpdateDto</param>
        /// <returns>FixedAsset</returns>
        /// Author: nxhinh (11/09/2023)  
        public async override Task<FixedAsset> MapUpdateDtoToEntity(FixedAssetUpdateDto fixedAssetUpdateDto)
        {
            var fixedAsset = _mapper.Map<FixedAsset>(fixedAssetUpdateDto);

            if (fixedAssetUpdateDto.fixed_asset_code != null)
            {
                await _fixedAssetManager.CheckExistAsync(fixedAssetUpdateDto.fixed_asset_code, fixedAsset.fixed_asset_id);
            }

            // Cập nhật các trường mặc định
            fixedAsset.production_year = fixedAsset.start_using_date?.Year;

            // Cập nhật giá trị hao mòn
            var temp = fixedAsset.depreciation_value_year * (DateTime.Now.Year - fixedAsset.production_year);
            if (temp > fixedAsset.cost)
            {
                fixedAsset.accumulated_depreciation = fixedAsset.cost;
            }
            else if (temp < 0)
            {
                fixedAsset.accumulated_depreciation = 0;
            }
            else
            {
                fixedAsset.accumulated_depreciation = temp;
            }

            // Cập nhật giá trị còn lại
            fixedAsset.remaining_value = fixedAsset.cost - fixedAsset.accumulated_depreciation;
            // kiểm tra dữ liệu
            await _fixedAssetManager.ValidateData(fixedAsset);
            return fixedAsset;
        }


        /// <summary>
        /// Tạo mới nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task InsertMultiAsync(IEnumerable<FixedAssetCreateDto> fixedAssetDtosList)
        {
            // Mở transaction
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Lấy danh sách mã tài sản để kiểm tra xem đã tồn tại chưa
                var listCode = fixedAssetDtosList.Select(x => (string)(x.fixed_asset_code ?? "")).ToList();

                // Kiểm tra xem danh sách mã có giá trị trùng lặp không
                if (listCode.Count != listCode.Distinct().Count())
                {
                    throw new Domain.InvalidDataException("Mã tài sản không được trùng lặp.");
                }


                var fixedAssets = new List<FixedAsset>();

                // Chuyển Dto thành Entity
                foreach (var fixedAssetDto in fixedAssetDtosList)
                {
                    var fixedAsset = _mapper.Map<FixedAsset>(fixedAssetDto);

                    // Cập nhật các trường mặc định
                    fixedAsset.fixed_asset_id = Guid.NewGuid();
                    fixedAsset.tracked_year = DateTime.Now.Year;
                    fixedAsset.active = true;
                    fixedAsset.production_year = fixedAsset.start_using_date?.Year;
                    fixedAsset.modified_date = DateTime.Now;
                    fixedAsset.modified_by = "";
                    fixedAsset.created_date = DateTime.Now;
                    fixedAsset.created_by = "";

                    // Cập nhật giá trị hao mòn
                    var temp = fixedAsset.depreciation_value_year * (DateTime.Now.Year - fixedAsset.production_year + 1);
                    if (temp > fixedAsset.cost)
                    {
                        fixedAsset.accumulated_depreciation = fixedAsset.cost;
                    }
                    else
                    {
                        fixedAsset.accumulated_depreciation = temp;
                    }
                    // Cập nhật giá trị còn lại
                    fixedAsset.remaining_value = fixedAsset.cost - fixedAsset.accumulated_depreciation;

                    // Kiểm tra dữ liệu
                    await _fixedAssetManager.ValidateData(fixedAsset);

                    fixedAssets.Add(fixedAsset);
                }

                // Thêm mới danh sách tài sản
                await _fixedAssetRepository.InsertMultiAsync(fixedAssets);

                // Lấy mã tài sản lớn nhất
                var code = fixedAssetDtosList.Max(asset => asset.fixed_asset_code);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetUpdateDtosList">Danh sách bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task UpdateMultiAsync(IEnumerable<FixedAssetUpdateDto> fixedAssetUpdateDtosList)
        {
            var fixedAssets = _mapper.Map<IEnumerable<FixedAsset>>(fixedAssetUpdateDtosList);

            foreach (var fixedAsset in fixedAssets)
            {
                // Cập nhật các trường mặc định
                fixedAsset.tracked_year = DateTime.Now.Year;
                fixedAsset.active = true;
                fixedAsset.production_year = fixedAsset.start_using_date?.Year;
                fixedAsset.modified_date = DateTime.Now;
                fixedAsset.modified_by = "";

                fixedAsset.depreciation_value_year = fixedAsset.cost * (decimal)(fixedAsset.depreciation_rate ?? 0) / 100;

                // Cập nhật giá trị hao mòn
                var temp = fixedAsset.depreciation_value_year * (DateTime.Now.Year - fixedAsset.production_year + 1);
                if (temp > fixedAsset.cost)
                {
                    fixedAsset.accumulated_depreciation = fixedAsset.cost;
                }
                else
                {
                    fixedAsset.accumulated_depreciation = temp;
                }
                // Cập nhật giá trị còn lại
                fixedAsset.remaining_value = fixedAsset.cost - fixedAsset.accumulated_depreciation;

                // Kiểm tra dữ liệu
                await _fixedAssetManager.ValidateData(fixedAsset);
            }


            await _fixedAssetRepository.UpdateMultiAsync(fixedAssets);
        }
    }
}
