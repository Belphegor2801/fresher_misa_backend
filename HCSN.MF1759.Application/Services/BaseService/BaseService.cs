using AutoMapper;
using HCSN.MF1759.Domain;
using System.Reflection.Metadata.Ecma335;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Base service có thể thực hiện các thao tác cơ bản với dữ liệu như thêm, sửa, xóa
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// <typeparam name="TEntityUpdateDto"></typeparam>
    /// Created by: nxhinh (11/09/2023) 
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyService<TEntity, TEntityDto>, IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Tạo bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi tạo</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public virtual async Task InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = await MapCreateDtoToEntity(entityCreateDto);

            if (entity is BaseAuditableEntity baseAuditEntity)
            {
                baseAuditEntity.modified_date = DateTime.Now;
                baseAuditEntity.modified_by = "";
                baseAuditEntity.created_date = DateTime.Now;
                baseAuditEntity.created_by = "";
            }

            // Mở transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Thực hiện thêm bản ghi
                await _baseRepository.InsertAsync(entity);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Bản ghi cập nhật</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public virtual async Task UpdateAsync(TEntityUpdateDto entityUpdateDto)
        {
            var entity = await MapUpdateDtoToEntity(entityUpdateDto);

            if (entity is BaseAuditableEntity baseAuditEntity)
            {
                baseAuditEntity.modified_date = DateTime.Now;
                baseAuditEntity.modified_by = "";
            }

            await _baseRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Xoá bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi cần xoá</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);

            await _baseRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id bản ghi cần xoá</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public virtual async Task DeleteMultiAsync(List<Guid> ids)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (ids.Count == 0)
                {
                    throw new Exception(ResourceVN.Error_NotEmptyList);
                }

                var entities = await _baseRepository.GetListByIdsAsync(ids);

                if (entities.ToList().Count < ids.Count)
                {
                    throw new Exception(ResourceVN.Error_CannotDelete);
                }

                await _baseRepository.DeleteMultiAsync(entities);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Chuyển từ create dto sang entity
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu dto</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public abstract Task<TEntity> MapCreateDtoToEntity(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Chuyển từ update dto sang entity
        /// </summary>
        /// <param name="entityUpdateDto">Dữ liệu dto</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public abstract Task<TEntity> MapUpdateDtoToEntity(TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Tạo code mới
        /// </summary>
        /// <param name="entity">Thực thể</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<String> AutoNewCode(TEntity entity)
        {
            var codes = await _baseRepository.GetAllCodeAsync();

            return CodeHandler.NewCode(codes);

        }
    }
}
