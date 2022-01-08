using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Framework.Attributes;

namespace ORM_Framework
{
    [Table("Barcode")]
    public class Barcode
    {
        [PrimaryKey("Id", false)]
        [Column("Id", DataType.INT)]
        public int Id { get; set; }

        [Column("Code", DataType.VARCHAR)]
        public string Code { get; set; }
    }
}
