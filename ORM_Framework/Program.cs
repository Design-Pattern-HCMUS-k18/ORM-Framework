﻿using ORM_Framework.SQL;
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
            //var db = DBConnectionFactory.CreateDBInstance(@"Data source=tcp:127.0.0.1;Database=ORM; Integrated Security=true", "sqlserver");
            //var list = db.ExecuteQueryWithoutRelationship<Product>("SELECT * FROM PRODUCT");
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

            var query = db.Select<Category>();
            //query.GroupBy("Name").Having("Sum(Price) > 4000000");
            query.Where("Id = 1");
            var list = query.Run();
            foreach (var category in list)
            {
                foreach (var product in category.Products)
                Console.WriteLine("{0} | {1}", product.Id, product.Name);
            }
        }
    }
}
