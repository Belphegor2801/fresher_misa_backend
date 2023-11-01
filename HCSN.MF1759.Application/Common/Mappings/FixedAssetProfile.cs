using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Mapping cho Tài sản
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class FixedAssetProfile : Profile
    {
        public FixedAssetProfile()
        {
            CreateMap<FixedAsset, FixedAssetDto>();
            CreateMap<FixedAssetCreateDto, FixedAsset>()
                .ForMember(dest => dest.fixed_asset_id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<FixedAssetUpdateDto, FixedAsset>();
        }
    }
}
