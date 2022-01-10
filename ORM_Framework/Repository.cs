using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public class Repository<T>
    {
        private readonly DBConnection _conn;
        public Repository(DBConnection cnn)
        {
            _conn = cnn;
        }
        public T? FindById(string id)
        {
            T? newObj = _conn.Select<T>().Where($"Id = '{id}'").Run().FirstOrDefault();
            return newObj;
        }
        public T? FindById(int id)
        {
            T? newObj = _conn.Select<T>().Where($"Id = {id}").Run().FirstOrDefault();
            return newObj;
        }
        public List<T> List()
        {
            return _conn.Select<T>().Run();
        }
        public void Delete(T obj)
        {
            if (obj == null) return;
            _conn.Delete(obj);
        }
        public int Update(T obj)
        {
            if (obj == null) return 0;
            return _conn.Update(obj);
        }
        public int Insert(T obj)
        {
            if (obj == null) return 0;
            return _conn.Insert(obj);
        }
    }
}
