using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public class SqlQuery
    {
        protected SqlCommand _command;
        protected string _query;
        private SQL_DBConnection _conn;
        public SqlQuery(SQL_DBConnection conn, string queryString)
        {
            _conn = conn;
            _command = (SqlCommand?)_conn.GetConnection().CreateCommand();
            _query = queryString;
        }

        public IEnumerable<T> ExecuteQueryWithoutRelationship<T>()
        {
            IList<T> list = new List<T>();
            Type type = typeof(T);
            _command.CommandText = _query;
            _conn.Open();
            var dataReader = _command.ExecuteReader();
            
            while (dataReader.Read())
            {
                // Chưa sử dụng attribute để mapping
                T obj = (T)Activator.CreateInstance(type);
                type.GetProperties().ToList().ForEach(p =>
                    p.SetValue(obj, dataReader[p.Name])
                );
                list.Add(obj);
            }
            _conn.Close();
            return list;
        }
    }
}
