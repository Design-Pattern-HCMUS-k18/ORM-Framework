using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class EntityAttribute : Attribute
    {
        public string Name { get; set; }
        public EntityAttribute()
        {
            Name = "";
        }
        public EntityAttribute(string name)
        {
            Name = name;
        }
    }
}
