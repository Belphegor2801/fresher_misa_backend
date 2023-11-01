using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCSN.MF1759.Domain
{
    /// <summary>
    /// Xử lý FilterObject
    /// </summary>
    /// Author: nxhinh (26/09/2023)
    public class FilterObjectHandler
    {
        /// <summary>
        /// Tạo điều kiện WHERE cho câu lệnh SQL dựa vào FilterObject
        /// </summary>
        /// <param name="filterObject"></param>
        /// <returns> Điều kiện where </returns>
        /// Author: nxhinh (26/09/2023)
        public static Task<string> CreateWhereCondition(FilterObject filterObject)
        {
            var sql = "WHERE";

            // search
            if (filterObject.Search?.Count > 0)
            {
                sql += "(";
                // biến kiểm tra sql thay đổi
                var sqlChange = false;
                foreach (var filterItem in filterObject.Search)
                {
                    if (!string.IsNullOrEmpty(filterItem.Value))
                    {
                        switch (filterItem.OperatorType)
                        {
                            case "LIKE":
                                sql += $" {filterItem.Field} LIKE '%{filterItem.Value}%' OR";
                                sqlChange = true;
                                break;
                            case "=":
                                sql += $" {filterItem.Field} = '{filterItem.Value}' OR";
                                sqlChange = true;
                                break;
                            default: break;
                        }
                    }
                }

                // Bỏ OR thừa và thêm ")"
                if (sqlChange)
                {
                    sql = sql.Substring(0, sql.Length - 2);
                    sql += ")";
                }
                else
                {
                    // Bỏ "("
                    sql = sql.Substring(0, sql.Length - 1);
                }
            }

            // filter
            if (filterObject.Filter?.Count > 0)
            {
                // Biến kiểm tra sql thay đổi
                var sqlChange = false;

                // Nối với lệnh trước đó
                if (sql != "WHERE")
                {
                    sql += " AND";
                    sqlChange = true;
                }

                foreach (var filterItem in filterObject.Filter)
                {
                    if (!string.IsNullOrEmpty(filterItem.Value))
                    {
                        switch (filterItem.OperatorType)
                        {
                            case "LIKE":
                                sql += $" {filterItem.Field} LIKE '%{filterItem.Value}%' AND";
                                sqlChange = true;
                                break;
                            case "=":
                                sql += $" {filterItem.Field} = '{filterItem.Value}' AND";
                                sqlChange = true;
                                break;
                            default: break;
                        }
                    }
                }
                // Bỏ AND thừa nếu sql đã thay đổi
                if (sqlChange)
                    sql = sql.Substring(0, sql.Length - 3);
            }

            if (sql == "WHERE" || sql == "WHERE AND") sql = "";
            return Task.FromResult(sql);
        }
    }
}
