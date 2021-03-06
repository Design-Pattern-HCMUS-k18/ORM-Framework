using ORM_Framework.Attributes;
using ORM_Framework.SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ORM_Framework
{
    public class SqlDeleteQuery<T> : SqlQuery
    {
        public SqlDeleteQuery(SqlConnection conn, T obj) : base(conn)
        {
            SqlMapper mapper = new SqlMapper();
            string tableName = AtributeUtils.GetIntance().GetTableName<T>();
            List<PrimaryKeyAttribute> pks = AtributeUtils.GetIntance().GetAllPrimaryKeys<T>();
            Dictionary<ColumnAttribute, object> columnValues = AtributeUtils.GetIntance().GetAllColumnValues<T>(obj);

            string valueStr = "";
            foreach (PrimaryKeyAttribute primaryKey in pks)
            {
                ColumnAttribute column = null;
                foreach (ColumnAttribute col in columnValues.Keys)
                {
                    if (col.Name == primaryKey.Name)
                        column = col;
                }
                if (column != null)
                {
                    string format = "{0} = {1}, ";
                    if (column.Type == DataType.NCHAR || column.Type == DataType.NVARCHAR)
                    {
                        format = "{0} = N'{1}', ";
                    }else if (column.Type == DataType.CHAR || column.Type == DataType.VARCHAR)
                    {
                        format = "{0} = '{1}', ";
                    }
                    valueStr += string.Format(format, primaryKey.Name, columnValues[column]);
                }
            }

            if (!string.IsNullOrEmpty(valueStr))
            {
                valueStr = valueStr.Substring(0, valueStr.Length - 2);
                _query = string.Format("DELETE {0} WHERE {1}", tableName, valueStr);
            }
        }
    }
}