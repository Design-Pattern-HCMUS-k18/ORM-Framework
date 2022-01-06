using ORM_Framework.Attributes;
using ORM_Framework.SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ORM_Framework
{
    public class SqlUpdate<T> : SqlQuery
    {
        public SqlUpdate(SqlConnection conn, T obj) : base(conn)
        {
            SqlMapper mapper = new SqlMapper();
            string tableName = mapper.GetTableName<T>();
            List<PrimaryKeyAttribute> pks = mapper.GetAllPrimaryKeys<T>();
            Dictionary<ColumnAttribute, object> listColumnValues = mapper.GetAllColumnValues<T>(obj);

            if (listColumnValues != null && pks != null)
            {
                string setStr = "";
                string whereStr = "";

                foreach (ColumnAttribute column in listColumnValues.Keys)
                {
                    string format = "{0} = {1}, ";
                    if (column.Type == DataType.NVARCHAR || column.Type == DataType.NCHAR)
                    {
                        format = "{0} = N'{1}', ";
                    }
                    else if (column.Type == DataType.VARCHAR || column.Type == DataType.CHAR)
                    {
                        format = "{0} = '{1}', ";
                    }
                    setStr += string.Format(format, column.Name, listColumnValues[column]);
                }
                if(!string.IsNullOrEmpty(setStr))
                {
                    setStr = setStr.Substring(0, setStr.Length - 2);
                }
                foreach (PrimaryKeyAttribute primaryKey in pks)
                {
                    //ColumnAttribute column = mapper.FindColumn(primaryKey.Name, listColumnValues);
                    ColumnAttribute column = null;
                    foreach(ColumnAttribute col in listColumnValues.Keys)
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
                        }
                        else if (column.Type == DataType.CHAR || column.Type == DataType.VARCHAR)
                        {
                            format = "{0} = '{1}', ";
                        }
                        whereStr += string.Format(format, primaryKey.Name, listColumnValues[column]);
                    }
                }
                if (!string.IsNullOrEmpty(whereStr))
                {
                    whereStr = whereStr.Substring(0, whereStr.Length - 2);
                    _query = string.Format("UPDATE {0} SET {1} WHERE {2}", tableName, setStr, whereStr);
                }
            }
        }
    }
}