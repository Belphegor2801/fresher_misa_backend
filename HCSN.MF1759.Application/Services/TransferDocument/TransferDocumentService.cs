using AutoMapper;
using HCSN.MF1759.Domain;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Service của chứng từ
    /// </summary>
    /// Created by: nxhinh (25/10/2023) 
    public class TransferDocumentService : BaseService<TransferDocument, TransferDocumentDto, TransferDocumentCreateDto, TransferDocumentUpdateDto>, ITransferDocumentService
    {
        private readonly ITransferDocumentRepository _transferDocumentRepository;
        private readonly ITransferDocumentManager _transferDocumentManager;
        private readonly ITransferDocumentDetailsRepository _transferDocumentDetailsRepository;
        private readonly ITransferDocumentDetailsManager _transferDocumentDetailsManager;
        private readonly IRecipientRepository _recipientRepository;
        private readonly IMapper _mapper;

        public TransferDocumentService(
            ITransferDocumentRepository transferDocumentRepository, 
            ITransferDocumentManager transferDocumentManager,
            ITransferDocumentDetailsRepository transferDocumentDetailsRepository,
            ITransferDocumentDetailsManager transferDocumentDetailsManager,
            IRecipientRepository recipientRepository,
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(transferDocumentRepository, mapper, unitOfWork)
        {
            _transferDocumentRepository = transferDocumentRepository;
            _transferDocumentManager = transferDocumentManager;
            _transferDocumentDetailsRepository = transferDocumentDetailsRepository;
            _transferDocumentDetailsManager = transferDocumentDetailsManager;
            _recipientRepository = recipientRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy danh sách tài sản được điều chuyển trong chứng từ
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023) 
        public virtual async Task<IEnumerable<TransferDocumentDetailsDto>> GetDetails(Guid document_id, FilterObject? filterObject)
        {
            filterObject.Limit = 0;
            var details = await _transferDocumentRepository.GetDetails(document_id, filterObject);

            var detailsDtos = _mapper.Map<IEnumerable<TransferDocumentDetailsDto>>(details);

            return detailsDtos;
        }


        /// <summary>
        /// Tạo code mới
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        public virtual async Task<string> NewCodeAsync()
        {
            var codes = await _transferDocumentRepository.GetAllCodeAsync();

            return CodeHandler.NewCode(codes);
        } 


        /// <summary>
        /// Chuyển từ TransferDocumentCreateDto sang TransferDocument
        /// </summary>
        /// <param name="transferDocumentCreateDto">TransferDocumentCreateDto</param>
        /// <returns>TransferDocument</returns>
        /// Author: nxhinh (25/10/2023)  
        public async override Task<TransferDocument> MapCreateDtoToEntity(TransferDocumentCreateDto transferDocumentCreateDto)
        {
            var transferDocument = _mapper.Map<TransferDocument>(transferDocumentCreateDto);

            if (transferDocument.document_code != null)
            {
                await _transferDocumentManager.CheckExistAsync(code: transferDocument.document_code, null);
            }

            // Cập nhật các trường mặc định
            transferDocument.document_id = Guid.NewGuid();

            // Kiểm tra dữ liệu
            await _transferDocumentManager.ValidateData(transferDocument);

            return transferDocument;
        }

        /// <summary>
        /// Chuyển từ TransferDocumentUpdateDto sang TransferDocument
        /// </summary>
        /// <param name="documentUpdateDto">TransferDocumentUpdateDto</param>
        /// <returns>TransferDocument</returns>
        /// Author: nxhinh (25/10/2023)  
        public async override Task<TransferDocument> MapUpdateDtoToEntity(TransferDocumentUpdateDto transferDocumentUpdateDto)
        {
            var transferDocument = _mapper.Map<TransferDocument>(transferDocumentUpdateDto);

            if (transferDocument.document_code != null)
            {
                await _transferDocumentManager.CheckExistAsync(transferDocument.document_code, transferDocument.document_id);
            }

            // kiểm tra dữ liệu
            await _transferDocumentManager.ValidateData(transferDocument);
            return transferDocument;
        }


        /// <summary>
        /// Tìm danh sách chứng từ của tài sản
        /// </summary>
        /// <param name="fixed_asset_id">id tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (08/11/2023)  
        public async Task<IEnumerable<TransferDocumentDto>> GetListByFixedAssetId(Guid fixed_asset_id)
        {
            var entities = await _transferDocumentRepository.GetListByFixedAssetId(fixed_asset_id);

            var entitieDtos = _mapper.Map<IEnumerable<TransferDocumentDto>>(entities);

            return entitieDtos;
        }

        /// <summary>
        /// Tìm danh sách chứng từ của tài sản sau thời gian chỉ định
        /// </summary>
        /// <param name="fixed_asset_id">id tài sản</param>
        /// <param name="date">thời gian</param>
        /// <returns></returns>
        /// Author: nxhinh (08/11/2023)  
        public async Task<IEnumerable<TransferDocumentDto>> GetListByFixedAssetIdAfterDate(Guid fixed_asset_id, DateTime date)
        {
            var entities = await _transferDocumentRepository.GetListByFixedAssetIdAfterDate(fixed_asset_id, date);

            var entitieDtos = _mapper.Map<IEnumerable<TransferDocumentDto>>(entities);

            return entitieDtos;
        }

        /// <summary>
        /// Tạo bản ghi
        /// </summary>
        /// <param name="documentCreateDto">Bản ghi tạo</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        override
        public async Task InsertAsync(TransferDocumentCreateDto documentCreateDto)
        {
            var document = await MapCreateDtoToEntity(documentCreateDto);

            var documentDetailsItems = _mapper.Map<List<TransferDocumentDetails>>(documentCreateDto.fixed_asset_list);

            var recipientItems = _mapper.Map<List<Recipient>>(documentCreateDto.recipients);

            if (document is BaseAuditableEntity baseAuditEntity)
            {
                baseAuditEntity.modified_date = DateTime.Now;
                baseAuditEntity.modified_by = "";
                baseAuditEntity.created_date = DateTime.Now;
                baseAuditEntity.created_by = "";
            }

            // Kiểm tra dữ liệu
            await _transferDocumentManager.ValidateData(document);

            foreach (var item in documentDetailsItems)
            {
                item.document_details_id = Guid.NewGuid();
                item.document_id = document.document_id;

                await _transferDocumentDetailsManager.ValidateData(document, item);
            }

            foreach (var item in recipientItems)
            {
                item.recipient_id = Guid.NewGuid();
                item.document_id = document.document_id;
            }


            // Mở transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Thực hiện thêm bản ghi
                await _transferDocumentRepository.InsertAsync(document);

                // Thêm details
                await _transferDocumentDetailsRepository.InsertMultiAsync(documentDetailsItems);

                await _recipientRepository.InsertMultiAsync(recipientItems);

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
        /// <param name="documentUpdateDto">Bản ghi cập nhật</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        override
        public async Task UpdateAsync(TransferDocumentUpdateDto documentUpdateDto)
        {
            var document = await MapUpdateDtoToEntity(documentUpdateDto);

            var filterObject = new FilterObject();
            filterObject.Limit = -1;
            // Danh sách tài sản bản đầu
            var documentDetails = await _transferDocumentRepository.GetDetails(document.document_id, filterObject);

            // Danh sách tài sản nhận được
            var documentDetailsUpdate = _mapper.Map<IEnumerable<TransferDocumentDetails>>(documentUpdateDto.fixed_asset_list);

            // Danh sách các tài sản nhận được so với dang sách ban đầu
            var documentDetailsToEdit = new List<TransferDocumentDetails>();
            var documentDetailsToAdd = new List<TransferDocumentDetails>();
            var documentDetailsToDelete = new List<TransferDocumentDetails>();

            // Danh sách thông tin giao nhận ban đầu
            var recipients = await _recipientRepository.GetListByDocumentId(document.document_id);
            // Danh sách thông tin giao nhận nhận được
            var recipientUpdate = _mapper.Map<IEnumerable<Recipient>>(documentUpdateDto.recipients);
            foreach (var item in recipientUpdate)
            {
                item.recipient_id = Guid.NewGuid();
                item.document_id = document.document_id;
            }

            foreach (var item in documentDetailsUpdate)
            {
               if (!documentDetails.Select(x => x.fixed_asset_id).Contains(item.fixed_asset_id))
                {
                    documentDetailsToAdd.Add(item);
                }
                else
                {
                    documentDetailsToEdit.Add(item);
                }
            }

            foreach (var item in documentDetails)
            {
                if (!documentDetailsUpdate.Select(x => x.fixed_asset_id).Contains(item.fixed_asset_id))
                {
                    documentDetailsToDelete.Add(item);
                }
                
            }

            if (document is BaseAuditableEntity baseAuditEntity)
            {
                baseAuditEntity.modified_date = DateTime.Now;
                baseAuditEntity.modified_by = "";
            }

            // Kiểm tra dữ liệu
            
            await _transferDocumentManager.ValidateData(document);

            foreach (var item in documentDetailsToAdd)
            {
                item.document_details_id = Guid.NewGuid();
                item.document_id = document.document_id;

                await _transferDocumentDetailsManager.ValidateData(document, item);
            }

            // Mở transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Thực hiện sửa bản ghi
                await _transferDocumentRepository.UpdateAsync(document);

                // Sửa tài sản
                await _transferDocumentDetailsRepository.UpdateMultiAsync(documentDetailsToEdit);

                // Xóa tài sản
                await _transferDocumentDetailsRepository.DeleteMultiAsync(documentDetailsToDelete);

                // Thêm tài sản mới
                await _transferDocumentDetailsRepository.InsertMultiAsync(documentDetailsToAdd);

                // Xóa recipient
                await _recipientRepository.DeleteMultiAsync(recipients);

                // Thêm mới recipient
                await _recipientRepository.InsertMultiAsync(recipientUpdate);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackAsync();
                throw;
            }
        }


        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id"> id bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        override
        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var document = await _transferDocumentRepository.GetAsync(id);

                var filterObject = new FilterObject();
                filterObject.Limit = -1;
                var documentDetails = await _transferDocumentRepository.GetDetails(document.document_id, filterObject);

                foreach (var item in documentDetails)
                {
                    await _transferDocumentDetailsManager.ValidateDelete(document, item);
                }

                await _transferDocumentDetailsRepository.DeleteMultiAsync(documentDetails);

                await _transferDocumentRepository.DeleteAsync(document);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        override
        public async Task DeleteMultiAsync(List<Guid> ids)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (ids.Count == 0)
                {
                    throw new Exception(ResourceVN.Error_NotEmptyList);
                }

                var documents = await _transferDocumentRepository.GetListByIdsAsync(ids);

                if (documents.ToList().Count < ids.Count)
                {
                    throw new Exception(ResourceVN.Error_CannotDelete);
                }

                foreach ( var document in documents)
                {
                    var filterObject = new FilterObject();
                    filterObject.Limit = -1;
                    var documentDetails = await _transferDocumentRepository.GetDetails(document.document_id, filterObject);

                    foreach (var item in documentDetails)
                    {
                        await _transferDocumentDetailsManager.ValidateDelete(document, item);
                    }

                    await _transferDocumentDetailsRepository.DeleteMultiAsync(documentDetails);
                }

                await _transferDocumentRepository.DeleteMultiAsync(documents);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackAsync();
                throw;
            }
        }
    }
}
