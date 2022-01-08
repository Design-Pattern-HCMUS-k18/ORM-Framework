using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public interface IQuery
    {
        public List<T> ExecuteQuery<T>() where T : new();
        public List<T> ExecuteQueryWithoutRelationship<T>();
        public int ExecuteNonQuery();
    }
}
