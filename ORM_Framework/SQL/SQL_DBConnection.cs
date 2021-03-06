using ORM_Framework.SQL;
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
            _connectionString = connectionString;
            _cnn = new SqlConnection(connectionString);
        }

        public SQL_DBConnection(SqlConnection cnn)
        {
            _cnn = cnn;
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

        public override List<T> ExecuteQueryWithoutMapping<T>(string query)
        {
            SqlQuery sqlQuery = new SqlQuery((SqlConnection)_cnn, query);
            return sqlQuery.ExecuteQueryWithoutMapping<T>();
        }

        public override List<T> ExecuteQueryAndMapping<T>(string query)
        {
            var sqlQuery = new SqlQuery((SqlConnection) _cnn, query);
            return sqlQuery.ExecuteQueryAndMapping<T>();
        }

        public override int Insert<T>(T obj)
        {
            SqlInsert<T> query = new SqlInsert<T>((SqlConnection)_cnn, obj);
            return query.ExecuteNonQuery();
        }
        public override int Update<T>(T obj)
        {
            SqlUpdate<T> query = new SqlUpdate<T>((SqlConnection)_cnn, obj);
            return query.ExecuteNonQuery();
        }
        public override void Delete<T>(T obj)
        {
            var query = new SqlDeleteQuery<T>((SqlConnection)_cnn, obj);
            query.ExecuteNonQuery();
        }

        public override IQueryBuilder<T> Select<T>(params string[] statements)
        {
            return new SqlSelect<T>(_cnn as SqlConnection, statements);
        }

        public override Repository<T> CreateRepository<T>()
        {
            return new Repository<T>(this);
        }
    }
}
