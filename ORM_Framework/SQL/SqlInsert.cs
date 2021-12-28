using ORM_Framework.Attributes;
using ORM_Framework.SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ORM_Framework
{
    public class SqlInsert<T> : SqlQuery
    {
        public SqlInsert(SqlConnection conn, T obj) : base(conn)
        {
            SqlMapper mapper = new SqlMapper();
            string tableName = mapper.GetTableName<T>();
            List<PrimaryKeyAttribute> pks = mapper.GetAllPrimaryKeys<T>();
            Dictionary<ColumnAttribute, object> columnValues = mapper.GetAllColumnValues<T>(obj);
            

            if(columnValues.Count > 0)
            {
                string columnStr = "";
                string valueStr = "";
                foreach(ColumnAttribute column in columnValues.Keys)
                {
                    bool isAutoIncrement = false;
                    // check column is primary key
                    foreach (PrimaryKeyAttribute primaryKey in pks)
                    {
                        if(primaryKey.Name == column.Name && primaryKey.AutoIncrement == true)
                        {
                            isAutoIncrement = true;
                        }
                    }

                    if(!isAutoIncrement)
                    {
                        string format = "{0}, ";
                        if(column.Type == DataType.NVARCHAR || column.Type == DataType.NCHAR)
                        {
                            format = "N'{0}', ";
                        } else if (column.Type == DataType.VARCHAR || column.Type == DataType.CHAR)
                        {
                            format = "'{0}', ";
                        }
                        columnStr += string.Format("{0}, ", column.Name);
                        valueStr += string.Format(format, columnValues[column]);
                    }
                }
                if (!string.IsNullOrEmpty(columnStr) && !string.IsNullOrEmpty(valueStr))
                {
                    columnStr = columnStr.Substring(0, columnStr.Length - 2);
                    valueStr = valueStr.Substring(0, valueStr.Length - 2);
                    _query = string.Format("INSERT INTO {0} ({1}) VALUES({2})", tableName, columnStr, valueStr);
                }
            }
        }
    }
}