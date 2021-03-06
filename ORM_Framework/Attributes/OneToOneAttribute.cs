using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class OneToOneAttribute : Attribute
    {
        public string ReferenceTable { get; set; }
        public OneToOneAttribute(string referenceTable)
        {
            ReferenceTable = referenceTable;
        }
    }
}
