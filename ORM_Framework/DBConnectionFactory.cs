using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public class DBConnectionFactory
    {
        public static DBConnection CreateDBInstance(string connectionString, string dialect)
        {
            switch(dialect)
            {
               case "sqlserver":
                    return new SQL_DBConnection(connectionString);
                default:
                    return null;
            }
        }
    }
}
