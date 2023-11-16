using Dapper;
using HCSN.MF1759.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace HCSN.MF1759.Infrastructure.Repositories
{
    /// <summary>
    /// Repository chi tiết chứng từ
    /// </summary>
    /// Created by: nxhinh (25/10/2023) 
    public class TransferDocumentDetailsRepository : BaseRepository<TransferDocumentDetails>, ITransferDocumentDetailsRepository
    {
        public override string TableName { get; protected set; } = "document_details";
        public override string TableKey { get; protected set; } = "document_details_id";


        public TransferDocumentDetailsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
        }

        /// <summary>
        /// Lấy danh sách tài sản điều chuyển trong chứng từ
        /// </summary>
        /// <param name="document_id">id chứng từ</param>
        /// <param name="filterObject">filter</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        public async Task<IEnumerable<TransferDocumentDetails>> GetListByDocumentId(Guid document_id, FilterObject? filterObject)
        {
            var query = $"Proc_document_details_GetListByDocumentId";

            var param = new DynamicParameters();

            param.Add("$document_id", document_id);

            param.Add("$columns", filterObject?.Columns != null ? filterObject.Columns : "*");
            param.Add("$sort_field", filterObject?.SortField != null ? filterObject.SortField : $"modified_date");
            param.Add("$sort_type", filterObject?.SortType != null ? filterObject.SortType : "ASC");
            param.Add("$offset", filterObject?.Offset != 0 ? filterObject.Offset : 0);
            param.Add("$limit", filterObject?.Limit != 0 ? filterObject.Limit : 20);

            var whereCondition = await FilterObjectHandler.CreateWhereCondition(filterObject);

            param.Add("$where_condition", whereCondition);

            // await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction);

            var result = await _unitOfWork.Connection.QueryAsync<TransferDocumentDetails>(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Lấy tổng số bản ghi theo Id chứng từ
        /// </summary>
        /// <param name="document_id">id chứng từ</param>
        /// <param name="filterObject">filter</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        public async Task<int> GetTotalRecordsByDocumentId(string document_id, FilterObject filterObject)
        {
            var query = $"Proc_document_details_GetTotalRecordsByDocumentId";

            var param = new DynamicParameters();

            param.Add("$document_id", document_id);

            param.Add("$columns", filterObject?.Columns != null ? filterObject.Columns : "*");
            param.Add("$sort_field", filterObject?.SortField != null ? filterObject.SortField : $"fixed_asset_code");
            param.Add("$sort_type", filterObject?.SortType != null ? filterObject.SortType : "ASC");
            param.Add("$offset", filterObject?.Offset != 0 ? filterObject.Offset : 0);
            param.Add("$limit", filterObject?.Limit != 0 ? filterObject.Limit : 20);

            var whereCondition = await FilterObjectHandler.CreateWhereCondition(filterObject);

            param.Add("$where_condition", whereCondition);

            // await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction);

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }


        /// <summary>
        /// Tìm danh sách điều chuyển của tài sản
        /// </summary>
        /// <param name="fixed_asset_id">id tài sản</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        public async Task<IEnumerable<TransferDocumentDetails>> GetListByFixedAssetId(Guid fixed_asset_id)
        {
            var query = $"Proc_document_details_GetListByFixedAssetId";

            var param = new DynamicParameters();

            param.Add("$fixed_asset_id", fixed_asset_id);

            // await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction);

            var result = await _unitOfWork.Connection.QueryAsync<TransferDocumentDetails>(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
