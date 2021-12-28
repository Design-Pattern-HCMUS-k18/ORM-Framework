using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Framework.Attributes;

namespace ORM_Framework
{
    [Entity(Name = "product")]
    public class Product
    {
        [PrimaryColumn(Name = "id", Type = Enum.DataType.INT, AutoGenarated = true)]
        public int Id { get; set; }
        [Column(Name = "name", Type = Enum.DataType.VARCHAR)]
        public string Name { get; set; }
        [Column(Name = "name", Type = Enum.DataType.FLOAT)]
        public decimal Price { get; set; }
    }
}
