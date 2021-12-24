using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SQL_DBConnection db = new SQL_DBConnection(@"Data source=DESKTOP-R463O3I\SQLEXPRESS;Database=ORM;User ID=sa;Password=123456; Integrated Security=true");
            //SqlQuery query = new SqlQuery(db, "SELECT * FROM PRODUCT");
            //var list = query.ExecuteQueryWithoutRelationship<Product>();

            //foreach(var product in list)
            //{
            //    Console.WriteLine(product.Name);
            //}

            SqlQuery query = new SqlQuery(db, "Insert into Product values(@Name, @Price)");
            int rowsEffected = query
                .AddParameter<string>("Name", "Bot giat")
                .AddParameter<decimal>("Price", 20000)
                .ExecuteNonQuery();
            Console.WriteLine(rowsEffected);
        }
    }
}
