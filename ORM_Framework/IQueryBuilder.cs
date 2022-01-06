using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public interface IQueryBuilder
    {
        IQueryBuilder Where(string condition);
        IQueryBuilder GroupBy(string columnNames);
        IQueryBuilder Having(string condition);
    }
}
