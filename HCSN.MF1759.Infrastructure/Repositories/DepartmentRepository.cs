using HCSN.MF1759.Domain;
using Dapper;

namespace HCSN.MF1759.Infrastructure
{
    /// <summary>
    /// Repository của phòng ban
    /// </summary>
    /// Created by: nxhinh (11/09/2023) 
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public override string TableName { get; protected set; } = "department";

        public override string TableCode { get; protected set; } = "department_code";
        public override string TableKey { get; protected set; } = "department_id";

        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
