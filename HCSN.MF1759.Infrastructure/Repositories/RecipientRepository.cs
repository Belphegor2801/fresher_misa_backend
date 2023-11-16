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
    /// Repository thông tin giao nhận
    /// </summary>
    /// Created by: nxhinh (25/10/2023) 
    public class RecipientRepository : BaseRepository<Recipient>, IRecipientRepository
    {
        public override string TableName { get; protected set; } = "recipient";
        public override string TableKey { get; protected set; } = "recipient_id";


        public RecipientRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        /// <summary>
        /// Lấy danh sách thông tin giao nhận trong chứng từ
        /// </summary>
        /// <param name="document_id">id chứng từ</param>
        /// <returns></returns>
        /// Author: nxhinh (25/10/2023)  
        public async Task<IEnumerable<Recipient>> GetListByDocumentId(Guid document_id)
        {
            var query = $"Proc_recipient_GetListByDocumentId";

            var param = new DynamicParameters();

            param.Add("$document_id", document_id);

            // await _unitOfWork.Connection.ExecuteAsync(query, param, transaction: _unitOfWork.Transaction);

            var result = await _unitOfWork.Connection.QueryAsync<Recipient>(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Lấy thông tin giao nhận được thêm vào lần cuối
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (14/11/2023)  
        public async Task<IEnumerable<Recipient>> GetLast()
        {
            var query = $"Proc_recipient_GetLast";
            var param = new DynamicParameters();

            var result = await _unitOfWork.Connection.QueryAsync<Recipient>(query, param, transaction: _unitOfWork.Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
