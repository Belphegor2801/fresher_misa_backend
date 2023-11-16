using AutoMapper;
using HCSN.MF1759.Domain;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Service của thông tin giao nhận
    /// </summary>
    /// Created by: nxhinh (25/10/2023) 
    public class RecipientService : BaseService<Recipient, RecipientDto, RecipientCreateDto, RecipientUpdateDto>, IRecipientService
    {
        private readonly IRecipientRepository _recipientRepository;
        private readonly IMapper _mapper;

        public RecipientService(
            IRecipientRepository recipientRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(recipientRepository, mapper, unitOfWork)
        {
            _recipientRepository = recipientRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tìm danh sách thông tin giao nhận theo id chứng từ
        /// </summary>
        /// <param name="document_id">id tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (08/11/2023)  
        public async Task<IEnumerable<RecipientDto>> GetListByDocumentId(Guid document_id)
        {
            var entities = await _recipientRepository.GetListByDocumentId(document_id);

            var entitieDtos = _mapper.Map<IEnumerable<RecipientDto>>(entities);

            return entitieDtos;
        }

        /// <summary>
        /// Lấy thông tin giao nhận được thêm vào lần cuối
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (14/11/2023)  
        public async Task<IEnumerable<RecipientDto>> GetLast()
        {
            var entities = await _recipientRepository.GetLast();

            var entitieDtos = _mapper.Map<IEnumerable<RecipientDto>>(entities);

            return entitieDtos;
        }

        /// <summary>
        /// Chuyển từ RecipientCreateDto sang Recipient
        /// </summary>
        /// <param name="recipientCreateDto">RecipientCreateDto</param>
        /// <returns>Recipient</returns>
        /// Author: nxhinh (25/10/2023) 
        public async override Task<Recipient> MapCreateDtoToEntity(RecipientCreateDto recipientCreateDto)
        {
            var recipient = _mapper.Map<Recipient>(recipientCreateDto);


            // Cập nhật các trường mặc định
            recipient.recipient_id = Guid.NewGuid();

            // Kiểm tra dữ liệu

            return recipient;
        }

        /// <summary>
        /// Chuyển từ RecipientUpdateDto sang Recipient
        /// </summary>
        /// <param name="entityUpdateDto">RecipientUpdateDto</param>
        /// <returns>Recipient</returns>
        /// Author: nxhinh (25/10/2023) 
        public async override Task<Recipient> MapUpdateDtoToEntity(RecipientUpdateDto recipientUpdateDto)
        {
            var recipient = _mapper.Map<Recipient>(recipientUpdateDto);


            // kiểm tra dữ liệu
            ///await _recipientManager.ValidateData(recipient);
            return recipient;
        }
    }
}
