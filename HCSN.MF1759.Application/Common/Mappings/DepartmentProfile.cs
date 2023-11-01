using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application.Mapper
{
    /// <summary>
    /// Mapping cho phòng ban
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>()
                .ForMember(m => m.department_id, o => o.MapFrom(s => Guid.NewGuid()));
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
