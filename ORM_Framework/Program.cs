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
            var db = DBConnectionFactory.CreateDBInstance(@"Data source=tcp:127.0.0.1;Database=ORM;User ID=sa;Password=SqlServer@1234; Integrated Security=false", "sqlserver");
            var list = db.ExecuteQueryWithoutRelationship<Product>("SELECT * FROM PRODUCT");
            foreach (var product in list)
            {
                Console.WriteLine(product.Name);
                //product.Name = "Xe máy";
                //Console.WriteLine(db.Update(product));
                db.Delete(product);
            }

            //Insert
            //Product product = new Product() {Id = 1, Name = "Xe đạp", Price = 4000000 };
            //int rowEffected = db.Insert(product);
            //Console.Write(rowEffected);

            //Update

        }
    }
}
