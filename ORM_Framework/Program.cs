using ORM_Framework.SQL;
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
            //var db = DBConnectionFactory.CreateDBInstance(@"Data source=DESKTOP-R463O3I\SQLEXPRESS;Database=ORM;User ID=sa;Password=123456; Integrated Security=true", "sqlserver");
            //var db = DBConnectionFactory.CreateDBInstance(@"Data source=tcp:127.0.0.1;Database=ORM; Integrated Security=true", "sqlserver");
            var db = DBConnectionFactory.CreateDBInstance(@"Data source=tcp:127.0.0.1;Database=ORM;User ID=sa;Password=SqlServer@1234; Integrated Security=false", "sqlserver");
            //var list = db.ExecuteQueryWithoutMapping<Product>("SELECT * FROM PRODUCT");
            //foreach (var product in list)
            //{
            //    Console.WriteLine(product.Name);
            //    //product.Name = "Xe máy";
            //    //Console.WriteLine(db.Update(product));
            //    db.Delete(product);
            //}

            //Insert
            //Product product = new Product() { Id = 2, Name = "Xe máy", Price = 4000000 };
            //int rowEffected = db.Insert(product);
            //Console.Write(rowEffected);

            //var query = db.Select<Product>();

            //var list = query.Run();
            //var repo = db.CreateRepository<Product>();
            //var list = repo.List();
            //var p = repo.FindById(1);
            //Console.WriteLine(p.Name);
            //foreach (var product in list)
            //{
            //    Console.WriteLine(product.Name);
            //    //product.Name = "Xe máy";
            //    //Console.WriteLine(db.Update(product));
            //}
            //var list = db.ExecuteQueryAndMapping<Category>("select * from category");
            var repo = db.CreateRepository<Category>();
            var list = repo.List();
            foreach (var category in list)
            {
                foreach (var product in category.Products)
                    Console.WriteLine("{0} | {1}", product.Id, product.Name);
            }
        }
    }
}
