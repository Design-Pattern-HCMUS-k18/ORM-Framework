using ORM_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey("Id", false)]
        [Column("Id", DataType.INT)]
        public int Id { get; set; }
        [Column("Name", DataType.NVARCHAR)]
        public string Name { get; set; }
        [OneToMany("Product")]
        public List<Product> Products { get; set; }
    }
}
