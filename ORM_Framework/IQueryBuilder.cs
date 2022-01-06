using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public interface IQueryBuilder<T>
    {
        IQueryBuilder<T> Where(string condition);
        IQueryBuilder<T> GroupBy(string columnNames);
        IQueryBuilder<T> Having(string condition);
        List<T> Run();
    }
}
