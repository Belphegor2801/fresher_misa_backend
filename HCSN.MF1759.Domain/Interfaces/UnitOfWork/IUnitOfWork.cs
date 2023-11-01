using System.Data.Common;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Interface của UnitOfWork
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        DbConnection Connection { get; }
        DbTransaction? Transaction { get; }

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// Author: nxhinh (11/09/2023)  
        void BeginTransaction();

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// Author: nxhinh (11/09/2023)  
        Task BeginTransactionAsync();

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// Author: nxhinh (11/09/2023)  
        void Commit();

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// Author: nxhinh (11/09/2023)  
        Task CommitAsync();

        /// <summary>
        /// Roll back transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        void RollBack();

        /// <summary>
        /// Roll back transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        Task RollBackAsync();
    }
}
