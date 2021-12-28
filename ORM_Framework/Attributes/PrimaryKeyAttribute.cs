using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class PrimaryKeyAttribute : Attribute
    {
        public string Name { get; set; }
        public bool AutoIncrement { get; set; }

        public PrimaryKeyAttribute(string _name, bool _isIncrement)
        {
            Name = _name;
            AutoIncrement = _isIncrement;
        }
    }
}
