using HCSN.MF1759.Domain;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Quản lý nghiệp vụ của phòng ban
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Kiểm tra mã phòng ban đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã phòng ban</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">Lỗi trả về nếu đã tồn tại</exception>
        /// Author: nxhinh (22/09/2023)  
        public async Task CheckExistByCodeAsync(string code)
        {
            var department = await _departmentRepository.FindByCodeAsync(code);

            if (department != null)
            {
                throw new ConflictException(ResourceVN.Error_DuplicateDepartmentCode);
            }
        }
    }
}
