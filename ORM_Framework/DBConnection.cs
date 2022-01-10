using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public abstract class DBConnection
    {
        protected IDbConnection _cnn;
        protected string _connectionString;
        public abstract void Open();
        public abstract void Close();

        public abstract int Insert<T>(T obj);
        public abstract int Update<T>(T obj);
        public abstract void Delete<T>(T obj);
        public abstract IQueryBuilder<T> Select<T>(params string[] statements);
        public abstract List<T> ExecuteQueryAndMapping<T>(string query);
        public abstract List<T> ExecuteQueryWithoutMapping<T>(string query);
        public abstract int ExecuteNonQuery<T>(string query);
        
    }
}
