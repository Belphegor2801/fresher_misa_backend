using Dapper;
using System.Data;
using HCSN.MF1759.Domain;
using HCSN.MF1759.Application;

namespace HCSN.MF1759.Infrastructure
{
    /// <summary>
    /// Base repository chỉ đọc
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: nxhinh (11/09/2023)
    public abstract class BaseReadOnlyRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Tên bảng
        /// </summary>
        public virtual string TableName { get; protected set; } = typeof(TEntity).Name.ToLower();

        /// <summary>
        /// Tên cột id
        /// </summary>
        public virtual string TableKey { get; protected set; } = typeof(TEntity).Name.ToLower() + "_id";

        /// <summary>
        /// Tên cột code
        /// </summary>
        public virtual string TableCode{ get; protected set; }

        public BaseReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            //var query = $"SELECT * FROM {TableName}";
           
            var query = $"Proc_{TableName}_GetAll";

            //var result = await _unitOfWork.Connection.QueryAsync<TEntity>(query, transaction: _unitOfWork.Transaction);
           
            var result = await _unitOfWork.Connection.QueryAsync<TEntity>(query, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// 
        /// Lấy bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Bản ghi theo id</returns>
        /// <exception cref="NotFoundException">Không tìm thấy trả về NotFoundException</exception>
        /// Author: nxhinh (11/09/2023)  
        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(ResourceVN.Error_Notfound);
            }

            return entity;
        }


        /// <summary>
        /// Lấy bản ghi theo code
        /// </summary>
        /// <param name="code">nã bản ghi</param>
        /// <returns>Bản ghi theo code</returns>
        /// <exception cref="NotFoundException">Không tìm thấy trả về NotFoundException</exception>
        /// Author: nxhinh (11/09/2023)  
        public async Task<TEntity> GetByCodeAsync(string code)
        {
            var entity = await GetByCodeAsync(code);

            if (entity == null)
            {
                throw new NotFoundException(ResourceVN.Error_Notfound);
            }

            return entity;
        }

        /// <summary>
        /// Lấy danh sách
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<IEnumerable<TEntity>> GetListAsync(FilterObject? filterObject)
        {
            var query = $"Proc_{TableName}_GetFilter";

            var param = new DynamicParameters();

            param.Add("$columns", filterObject?.Columns != null ? filterObject.Columns : "*");
            param.Add("$sort_field", filterObject?.SortField != null ? filterObject.SortField : $"modified_date");
            param.Add("$sort_type", filterObject?.SortType != null ? filterObject.SortType : "ASC");
            param.Add("$offset", filterObject?.Offset != 0 ? filterObject.Offset : 0);
            param.Add("$limit", filterObject?.Limit >= 0 ? filterObject.Limit : 20);

            var whereCondition = await FilterObjectHandler.CreateWhereCondition(filterObject);

            param.Add("$where_condition", whereCondition);

            var result = await _unitOfWork.Connection.QueryAsync<TEntity>(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            //var columns = filterObject?.Columns != null ? filterObject.Columns : "*";
            //var sortField = filterObject?.SortField != null ? filterObject.SortField : $"modified_date";
            //var sortType = filterObject?.SortType != null ? filterObject.SortType : "ASC";
            //var offset = filterObject?.Offset != 0 ? filterObject.Offset : 0;
            //var limit = filterObject?.Limit != -1 ? filterObject.Limit : 20;

            //var whereCondition = await FilterObjectHandler.CreateWhereCondition(filterObject);


            //var query = $"SELECT {columns} FROM {TableName} " +
            //    $"{whereCondition}" +
            //    $"ORDER BY {sortField} {sortType} ";

            //if (filterObject.Limit != 0)
            //    query += $"LIMIT {limit} OFFSET {offset} ";

            //var result = await _unitOfWork.Connection.QueryAsync<TEntity>(query, transaction: _unitOfWork.Transaction);

            return result;
        }

        /// <summary>
        /// Tìm bản ghi theo id
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Bản ghi theo id</returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<TEntity?> FindAsync(Guid id)
        {
            //var query = $"SELECT * FROM {TableName} WHERE {TableKey} = @id;";

            var query = $"Proc_{TableName}_Get";

            var param = new DynamicParameters();

            param.Add("$id", id);

            //var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(query, param, transaction: _unitOfWork.Transaction);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo list ids
        /// </summary>
        /// <param name="ids">Danh sách id</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<IEnumerable<TEntity>> GetListByIdsAsync(List<Guid> ids)
        {
            var query = $"SELECT * FROM {TableName} WHERE {TableKey} IN @ids;";
            
            var param = new DynamicParameters();

            param.Add("ids", ids);

            var entities = await _unitOfWork.Connection.QueryAsync<TEntity>(query, param, transaction: _unitOfWork.Transaction);

            return entities;
        }

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> GetTotalRecordsAsync(FilterObject filterObject)
        {
            var whereCondition = await FilterObjectHandler.CreateWhereCondition(filterObject);

            //var query = $"SELECT COUNT(*) FROM {TableName} {whereCondition}";

            var query = $"Proc_{TableName}_GetTotalRecords";

            var param = new DynamicParameters();

            param.Add("$where_condition", whereCondition);

            //var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(query, transaction: _unitOfWork.Transaction);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Số bản ghi có mã hoặc id tương ứng
        /// </summary>
        /// <param name="code">Mã định danh</param>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Số bản ghi</returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task<int> CountByCodeOrId(string code, Guid? id)
        {
            var query = $"SELECT COUNT(*) FROM {TableName} WHERE {TableCode} = @code OR {TableKey} = @id;";

            var param = new DynamicParameters();

            param.Add("code", code);
            param.Add("id", id);

            var count = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(query, param, transaction: _unitOfWork.Transaction);

            return count;
        }

        /// <summary>
        /// Lấy toàn bộ mã
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023) 
        public async Task<IEnumerable<string>> GetAllCodeAsync()
        {
            if (TableCode == null)
            {
                throw new NotFoundException("Tài nguyên này không có mã code");
            }
            var query = $"SELECT {TableCode} FROM {TableName}";

            var result = await _unitOfWork.Connection.QueryAsync<string>(query, transaction: _unitOfWork.Transaction);

            return result;
        }


        /// <summary>
        /// Tìm bản ghi theo mã
        /// </summary>
        /// <param name="code">Mã bản ghi</param>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023) 
        public async Task<TEntity?> FindByCodeAsync(string code)
        {
            var query = $"SELECT * FROM {TableName} WHERE {TableCode} = @code";

            var param = new DynamicParameters();

            param.Add("code", code);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(query, param, transaction: _unitOfWork.Transaction);

            return result;
        }
    }
}
