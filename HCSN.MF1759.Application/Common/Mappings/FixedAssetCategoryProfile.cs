using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application.Mapper
{
    /// <summary>
    /// Mapping cho loại tài sản
    /// </summary>
    /// Author: nxhinh (22/09/2023) 
    public class FixedAssetCategoryProfile : Profile
    {
        public FixedAssetCategoryProfile()
        {
            CreateMap<FixedAssetCategory, FixedAssetCategoryDto>();
            CreateMap<FixedAssetCategoryCreateDto, FixedAssetCategory>();
            CreateMap<FixedAssetCategoryUpdateDto, FixedAssetCategory>();
        }
    }
}
