using ORM_Framework.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public DataType Type { get; set; }
        public ColumnAttribute()
        {
            Name = "";
            Type = DataType.VARCHAR;
        }
        public ColumnAttribute(string name)
        {
            Name = name;
            Type = DataType.VARCHAR;
        }
        public ColumnAttribute(string name, DataType type)
        {
            Name = name;
            Type = type;
        }
    }
}
