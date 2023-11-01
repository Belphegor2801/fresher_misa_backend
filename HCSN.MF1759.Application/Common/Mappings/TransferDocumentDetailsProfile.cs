using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Mapping chi tiết chứng từ
    /// </summary>
    /// Author: nxhinh (25/10/2023)  
    public class TransferDocumentDetailsProfile: Profile
    {
        public TransferDocumentDetailsProfile()
        {
            CreateMap<TransferDocumentDetails, TransferDocumentDetailsDto>();
            CreateMap<TransferDocumentDetailsCreateDto, TransferDocumentDetails>()
                .ForMember(dest => dest.document_details_id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<TransferDocumentDetailsUpdateDto, TransferDocumentDetails>();
        }
    }
}
