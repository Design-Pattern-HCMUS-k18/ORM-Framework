using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public class SQL_DBConnection : DBConnection
    {
        public SQL_DBConnection(string connectionString)
        {
            _cnn = new SqlConnection(connectionString);
        }
        public override void Open()
        {
            if (_cnn.State != System.Data.ConnectionState.Open)
            {
                _cnn.Open();
            }
        }
        public override void Close()
        {
            if(_cnn.State != System.Data.ConnectionState.Closed)
            {
                _cnn.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return (SqlConnection)_cnn;
        }

        public override int ExecuteNonQuery<T>(string query)
        {
            throw new NotImplementedException();
        }

        public override List<T> ExecuteQueryWithoutRelationship<T>(string query)
        {
            SqlQuery sqlQuery = new SqlQuery((SqlConnection)_cnn, query);
            return sqlQuery.ExecuteQueryWithoutRelationship<T>();
        }

        public override List<T> ExecuteQuery<T>(string query)
        {
            throw new NotImplementedException();
        }

        public override int Insert<T>(T obj)
        {
            SqlInsert<T> query = new SqlInsert<T>((SqlConnection)_cnn, obj);
            return query.ExecuteNonQuery();
        }
    }
}
