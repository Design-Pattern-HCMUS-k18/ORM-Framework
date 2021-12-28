using ORM_Framework.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class PrimaryColumnAttribute : ColumnAttribute
    {
        public bool AutoGenarated { get; set; }
        public PrimaryColumnAttribute() : base()
        {
            AutoGenarated = false;
        }
        public PrimaryColumnAttribute(string name) : base(name)
        {
            AutoGenarated = false;
        }
        public PrimaryColumnAttribute(string name, DataType type) : base(name, type)
        {
            AutoGenarated = false;
        }
        public PrimaryColumnAttribute(string name, DataType type, bool autogen) : base(name, type)
        {
            AutoGenarated = autogen;
        }
    }
}
