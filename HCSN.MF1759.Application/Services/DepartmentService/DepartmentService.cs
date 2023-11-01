using AutoMapper;
using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Application
{
    /// <summary>
    /// Service của phòng ban
    /// </summary>
    /// Created by: nxhinh (11/09/2023) 
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IDepartmentManager departmentManager, IUnitOfWork unitOfWork) : base(departmentRepository, mapper, unitOfWork)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Chuyển từ DepartmentCreateDto sang Department
        /// </summary>
        /// <param name="entityCreateDto">department cần chuyển</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public override Task<Department> MapCreateDtoToEntity(DepartmentCreateDto departmentCreateDto)
        {
            var department = _mapper.Map<Department>(departmentCreateDto);

            department.department_id = Guid.NewGuid();

            return Task.FromResult(department);
        }

        /// <summary>
        /// Chuyển từ DepartmentUpdateDto sang Department
        /// </summary>
        /// <param name="entityUpdateDto">department cần chuyển</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public override Task<Department> MapUpdateDtoToEntity(DepartmentUpdateDto departmentUpdateDto)
        {
            var department = _mapper.Map<Department>(departmentUpdateDto);

            return Task.FromResult(department);
        }
        #endregion
    }
}
