using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Mapping cho thông tin giao nhận
    /// </summary>
    /// Author: nxhinh (25/10/2023)  
    public class RecipientProfile : Profile
    {
        public RecipientProfile()
        {
            CreateMap<Recipient, RecipientDto>();
            CreateMap<RecipientCreateDto, Recipient>()
                .ForMember(dest => dest.recipient_id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<RecipientUpdateDto, Recipient>();
        }
    }
}
