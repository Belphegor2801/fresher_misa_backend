using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Mapping chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023)  
    public class TransferDocumentProfile: Profile
    {
        public TransferDocumentProfile()
        {
            CreateMap<TransferDocument, TransferDocumentDto>();
            CreateMap<TransferDocumentCreateDto, TransferDocument>()
                .ForMember(dest => dest.document_id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<TransferDocumentUpdateDto, TransferDocument>();
        }
    }
}
