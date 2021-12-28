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
        public abstract void Open();
        public abstract void Close();

        public abstract int Insert<T>(T obj);
        public abstract List<T> ExecuteQuery<T>(string query);
        public abstract List<T> ExecuteQueryWithoutRelationship<T>(string query);
        public abstract int ExecuteNonQuery<T>(string query);
        
    }
}
