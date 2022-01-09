using ORM_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Framework.Attributes;

namespace ORM_Framework
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey("Id", false)]
        [Column("Id", DataType.INT)]
        public int Id { get; set; }
        [Column("Name", DataType.NVARCHAR)]
        public string Name { get; set; }
        [Column("Price", DataType.DECIMAL)]
        public decimal Price { get; set; }

        [Column("BarcodeId", DataType.INT)]
        [ForeignKey("Barcode", "Id")]
        public int BarcodeId { get; set; }
        [Column("CategoryId", DataType.INT)]
        [ForeignKey("Category", "Id")]
        public int CategoryId { get; set; }
        [ManyToOne("Category")]
        public Category category { get; set; }
        [OneToOne("Barcode")]
        public Barcode barcode { get; set; }
    }
}
