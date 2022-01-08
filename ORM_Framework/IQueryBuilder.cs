using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public interface IQueryBuilder<T>
    {
        IQueryBuilder<T> Where(string firstCondition, params string[] conditions);
        IQueryBuilder<T> WhereOr(string firstCondition, params string[] conditions);
        IQueryBuilder<T> GroupBy(string firstColumnName, params string[] columnNames);
        IQueryBuilder<T> Having(string firstCondition, params string[] conditions);
        IQueryBuilder<T> HavingOr(string firstCondition, params string[] conditions);
        List<T> Run();
    }
}
