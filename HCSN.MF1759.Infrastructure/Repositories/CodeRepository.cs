using Dapper;
using HCSN.MF1759.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Infrastructure
{
    public class CodeRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CodeRepository(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Lấy mã mới
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <returns>Mã mới</returns>
        /// Created by: nxhinh (22/09/2023) 
        public async Task<string> GetNewCode(string entityName)
        {
            var codes = await GetAllCodeAsync(entityName);
            
            var code = CodeHandler.NewCode(codes);

            return code;
        }

        /// <summary>
        /// Lấy toàn bộ mã
        /// </summary>
        /// <returns></returns>
        /// Author: nxhinh (22/09/2023) 
        public async Task<List<string>> GetAllCodeAsync(string entityName)
        {
            var TableCode = entityName + "_code";
            var TableName = entityName + "_name";
            var query = $"SELECT {TableCode} FROM {TableName}";

            var param = new DynamicParameters();

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<List<string>>(query, param, transaction: _unitOfWork.Transaction);

            return result;
        }
    }
}
