using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public interface IQuery
    {
        public List<T> ExecuteQueryAndMapping<T>();
        public List<T> ExecuteQueryWithoutMapping<T>();
        public int ExecuteNonQuery();
    }
}
