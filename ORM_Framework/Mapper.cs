using ORM_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public abstract class Mapper
    {
        public List<PrimaryKeyAttribute> GetAllPrimaryKeys<T>()
        {
            List<PrimaryKeyAttribute> primaryKeys = new List<PrimaryKeyAttribute>();
            Type t = typeof(T);
            foreach(PropertyInfo property in t.GetProperties())
            {
                var attributes = property.GetCustomAttributes(false);
                foreach(var a in attributes)
                {
                    PrimaryKeyAttribute pk = a as PrimaryKeyAttribute;
                    if(pk != null)
                    {
                        primaryKeys.Add(pk);
                    }
                }
            }
            return primaryKeys;
        }

        public Dictionary<ColumnAttribute, object> GetAllColumnValues<T>(T obj)
        {
            Dictionary<ColumnAttribute,object> columns = new Dictionary<ColumnAttribute,object>();
            Type t = obj.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach(PropertyInfo p in properties)
            {
                var attributes = p.GetCustomAttributes(false);
                foreach(var attribute in attributes)
                {
                    ColumnAttribute c = attribute as ColumnAttribute;
                    if(c != null)
                    {
                        columns.Add(c, p.GetValue(obj));
                    }
                }
            }
            return columns;
        }

        public string GetTableName<T>()
        {
            Type t = typeof(T);
            var tableAttributes = t.GetCustomAttributes(typeof(TableAttribute), true);
            if(tableAttributes[0] != null)
            {
                TableAttribute tba = tableAttributes[0] as TableAttribute;
                return tba.Name;
            }
            return "";
        }

        public List<ColumnAttribute> GetColumns<T>()
        {
            Type t = typeof (T);
            PropertyInfo[] properties = t.GetProperties();
            var list = new List<ColumnAttribute>();
            foreach (PropertyInfo p in properties)
            {
                var attributes = p.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    ColumnAttribute c = attribute as ColumnAttribute;
                    if (c != null)
                    {
                        list.Add(c);
                    }
                }
            }
            return list;
        }

        public List<ForeignKeyAttribute> GetAllForeignKeyAttributes<T>(string referenceTable)
        {
            List<ForeignKeyAttribute> list = new List<ForeignKeyAttribute>();
            Type type = typeof (T);
            var properties = type.GetProperties();
            foreach(var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                foreach(var attribute in attributes)
                {
                    var foreignKey = attribute as ForeignKeyAttribute;
                    if(foreignKey != null && foreignKey.ReferenceTable.Equals(referenceTable))
                    {
                        list.Add(foreignKey);
                    }
                }
            }
            return list;
        }

        public List<ColumnAttribute> GetColumnAttributes<T>()
        {
            Type type = typeof(T);
            List<ColumnAttribute> list = new List<ColumnAttribute>();
            foreach(var property in type.GetProperties())
            {
                var attributes = property.GetCustomAttributes(false);
                for(int i = 0; i < attributes.Length; i++)
                {
                    var attribute = attributes[i];
                    var column = attribute as ColumnAttribute;
                    if(column != null)
                        list.Add(column);
                }
            }
            return list;
        }
    }
}
