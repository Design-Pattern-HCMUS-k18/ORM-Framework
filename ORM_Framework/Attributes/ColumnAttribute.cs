using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public DataType Type { get; set; }

        public ColumnAttribute()
        {

        }

        public ColumnAttribute(string _name, DataType _type)
        {
            Name = _name;
            Type = _type;
        }
    }
}
