using HCSN.MF1759.Domain;
using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace HCSN.MF1759.Infrastructure
{
    /// <summary>
    /// Unit of work
    /// </summary>
    /// Created by: nxhinh (11/09/2023)
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;

        private DbTransaction? _transaction = null;

        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        
        public DbConnection Connection => _connection;

        public DbTransaction? Transaction => _transaction;

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        public void BeginTransaction()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Khởi tạo transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        public async Task BeginTransactionAsync()
        {
            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }

            if (_transaction == null)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }

            await DisposeAsync();
        }

        /// <summary>
        /// Dispose transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;

            _connection.Close();
        }

        /// <summary>
        /// Dispose transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }

            await _connection.CloseAsync();
        }

        /// <summary>
        /// Rollback transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        public void RollBack()
        {
            _transaction?.Rollback();
            Dispose();
        }

        /// <summary>
        /// Rollback transaction
        /// </summary>
        /// Created by: nxhinh (11/09/2023) 
        public async Task RollBackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }

            await DisposeAsync();
        }
    }
}
