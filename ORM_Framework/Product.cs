using ORM_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey("Id", true)]
        [Column("Id", DataType.INT)]
        public int Id { get; set; }
        [Column("Name", DataType.NVARCHAR)]
        public string Name { get; set; }
        [Column("Price", DataType.DECIMAL)]
        public decimal Price { get; set; }
    }
}
