using Dapper;
using HCSN.MF1759.Domain;
using System.ComponentModel;
using System.Data;
using static Dapper.SqlMapper;

namespace HCSN.MF1759.Infrastructure
{
    /// <summary>
    /// Base repository thêm sửa xoá
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created by: nxhinh (11/09/2023)
    public abstract class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : IHaskey
    {

        protected BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {}

        /// <summary>
        /// Tạo 1 entity mới
        /// </summary>
        /// <param name="entity">Entity tạo</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task InsertAsync(TEntity entity)
        {
            var properties = typeof(TEntity).GetProperties();

            var columns = string.Join(",", properties.Select(x => x.Name).ToList());

            var values = string.Join(",", properties.Select(x => '@' + x.Name).ToList());

            // var query = $"INSERT INTO {TableName} ({columns}) VALUES ({values}) ";

            var query = $"Proc_{TableName}_Insert";

            var param = new DynamicParameters();

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add("$" + propertyName, propertyValue);
            }

            // await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction);
            
            await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Tạo nhiều entity mới
        /// </summary>
        /// <param name="entities">Danh sách entity</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task InsertMultiAsync(IEnumerable<TEntity> entities)
        {
            var properties = typeof(TEntity).GetProperties().Where(
                prop => !Attribute.IsDefined(prop, typeof(OnlyForGetAttribute)));

            var columns = string.Join(",", properties.Select(x => x.Name).ToList());

            var values = string.Join(",", properties.Select(x => $"@{x.Name}").ToList());

            var query = $"INSERT INTO {TableName} ({columns}) VALUES ({values}) ";

            /// var query = $"Proc_{TableName}_InsertMany";

            // Tạo danh sách các tham số
            var listParam = new List<DynamicParameters>();

            foreach (var entity in entities)
            {
                var param = new DynamicParameters();

                foreach (var property in properties)
                {
                    param.Add(property.Name, property.GetValue(entity));
                }

                listParam.Add(param);
            }

            await _unitOfWork.Connection.ExecuteAsync(query, listParam, transaction: _unitOfWork.Transaction);

            /// await _unitOfWork.Connection.ExecuteAsync(query, listParam, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Sửa 1 entity
        /// </summary>
        /// <param name="entity">Entity sửa</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task UpdateAsync(TEntity entity)
        {
            var entiryTemp = await FindAsync(entity.GetKey());

            if (entiryTemp == null)
            {
                throw new NotFoundException("Tài nguyên không tồn tại!");
            }


            var properties = typeof(TEntity).GetProperties();

            var set = string.Join(",", properties.Select(x => $"{x.Name} = @{x.Name}").ToList());

            // var query = $"UPDATE {TableName} SET {set} WHERE {TableKey} = @{TableKey} ";

            var query = $"Proc_{TableName}_Update";

            var param = new DynamicParameters();

            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add("$" + propertyName, propertyValue);
            }

            // await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction);

            await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

        }

        /// <summary>
        /// Sửa nhiều entity
        /// </summary>
        /// <param name="entities">Danh sách entity</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task UpdateMultiAsync(IEnumerable<TEntity> entities)
        {
            var properties = typeof(TEntity).GetProperties().Where(
                prop => !Attribute.IsDefined(prop, typeof(OnlyForGetAttribute)));

            var set = string.Join(",", properties.Select(x => $"{x.Name} = @{x.Name}").ToList());

            var query = $"UPDATE {TableName} SET {set} WHERE {TableKey} = @{TableKey} ";

            /// var query = $"Proc_{TableName}_UpdateMany";

            // Tạo danh sách các tham số
            var listParam = new List<DynamicParameters>();

            foreach (var entity in entities)
            {
                var param = new DynamicParameters();

                foreach (var property in properties)
                {
                    param.Add(property.Name, property.GetValue(entity));
                }

                listParam.Add(param);
            }

            await _unitOfWork.Connection.ExecuteAsync(query, listParam, transaction: _unitOfWork.Transaction);

            /// await _unitOfWork.Connection.ExecuteAsync(query, listParam, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Xoá 1 entity
        /// </summary>
        /// <param name="entity">Entity xoá</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task DeleteAsync(TEntity entity)
        {
            //var query = $"DElETE From {TableName} WHERE {TableKey} = @id;";

            var query = $"Proc_{TableName}_Delete";

            var param = new DynamicParameters();

            param.Add("$id", entity.GetKey());

            // await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction);

            await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Xoá nhiều entity
        /// </summary>
        /// <param name="entities">Danh sách entity xoá</param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        public async Task DeleteMultiAsync(IEnumerable<TEntity> entities)
        {
            var query = $"DElETE From {TableName} WHERE {TableKey} IN @ids;";
            //var query = $"Proc_{TableName}_DeleteMany";

            var param = new DynamicParameters();

            param.Add("ids", entities.Select(x => x.GetKey()));

            await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction);

            // await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
