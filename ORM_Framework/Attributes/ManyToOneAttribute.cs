using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ManyToOneAttribute : Attribute
    {
        public ManyToOneAttribute(string table)
        {
            this.ReferenceTable = table;
        }

        public string ReferenceTable { get; set; }
    }
}
