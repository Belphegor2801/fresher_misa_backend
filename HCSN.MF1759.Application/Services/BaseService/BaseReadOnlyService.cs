using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Base service chỉ đọc
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// Author: nxhinh (11/09/2023)  
    public abstract class BaseReadOnlyService<TEntity, TEntityDto> : IBaseReadOnlyService<TEntityDto>
    {
        protected readonly IBaseReadOnlyRepository<TEntity> _readOnlyRepository;
        protected readonly IMapper _mapper;

        protected BaseReadOnlyService(IBaseReadOnlyRepository<TEntity> readOnlyRepository, IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entities = await _readOnlyRepository.GetAllAsync();

            var entitieDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);

            return entitieDtos;
        }

        /// <summary>
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _readOnlyRepository.GetAsync(id);

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }


        /// <summary>
        /// Lấy bản ghi theo code
        /// </summary>
        /// <param name="code">code bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<TEntityDto> GetByCodeAsync(string code)
        {
            var entity = await _readOnlyRepository.FindByCodeAsync(code);

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }



        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<IEnumerable<TEntityDto>> GetListAsync(FilterObject? filterObject)
        {
            var entities = await _readOnlyRepository.GetListAsync(filterObject);

            var entitieDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);

            return entitieDtos;
        }

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<TEntityDto?> FindAsync(Guid id)
        {
            var entity = await _readOnlyRepository.FindAsync(id);

            var entityDto = _mapper.Map<TEntityDto?>(entity);

            return entityDto;
        }

        /// <summary>
        /// Lấy tất cả mã code
        /// </summary>
        /// <returns>Danh sách mã code</returns>
        /// Created by: nxhinh (11/09/2023) 
        public async Task<IEnumerable<string>> GetAllCodeAsync()
        {
            var codes = await _readOnlyRepository.GetAllCodeAsync();

            return codes;
        }

        /// <summary>
        /// Lấy tổng số bản ghi.
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<int> GetTotalRecordsAsync(FilterObject filterObject)
        {

            var totalRecords = await _readOnlyRepository.GetTotalRecordsAsync(filterObject);

            return totalRecords;
        }

        /// <summary>
        /// Lấy bản ghi theo danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<IEnumerable<TEntityDto>> GetListByIdsAsync(List<Guid> ids)
        {
            var entities = await _readOnlyRepository.GetListByIdsAsync(ids);
            var entityDtos = _mapper.Map<IEnumerable<TEntityDto>>(entities);

            return entityDtos;
        }
    }
}
