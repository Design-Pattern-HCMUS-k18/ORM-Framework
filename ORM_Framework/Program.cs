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

            // Query
            //var list = db.ExecuteQueryWithoutRelationship<Product>("SELECT * FROM PRODUCT");
            //foreach (var product in list)
            //{
            //    Console.WriteLine(product.Name);
            //}

            //Insert
            Product product = new Product() { Name = "Quạt", Price = 400000 };
            int rowEffected = db.Insert(product);
            Console.Write(rowEffected);
        }
    }
}
