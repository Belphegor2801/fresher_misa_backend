using Microsoft.AspNetCore.Mvc;
using HCSN.MF1759.Application;

namespace HCSN.MF1759.Controllers
{
    /// <summary>
    /// Controller cho phòng ban
    /// </summary>
    /// Created by: nxhinh (11/09/2023) 
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentController : BaseController<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        }
    }
}
