using ORM_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public class AtributeUtils
    {
        private static AtributeUtils? _instance;
        private AtributeUtils()
        {

        }
        public static AtributeUtils GetIntance()
        {
            if (_instance == null)
                _instance = new AtributeUtils();
            return _instance;
        }
        public List<PrimaryKeyAttribute> GetAllPrimaryKeys<T>()
        {
            List<PrimaryKeyAttribute> primaryKeys = new List<PrimaryKeyAttribute>();
            Type t = typeof(T);
            foreach (PropertyInfo property in t.GetProperties())
            {
                var attributes = property.GetCustomAttributes(false);
                foreach (var a in attributes)
                {
                    PrimaryKeyAttribute? pk = a as PrimaryKeyAttribute;
                    if (pk != null)
                    {
                        primaryKeys.Add(pk);
                    }
                }
            }
            return primaryKeys;
        }
        public PrimaryKeyAttribute? GetPrimaryKey<T>(string columnName)
        {
            Type t = typeof(T);
            foreach (PropertyInfo property in t.GetProperties())
            {
                var attributes = property.GetCustomAttributes(false);
                foreach (var a in attributes)
                {
                    PrimaryKeyAttribute? pk = a as PrimaryKeyAttribute;
                    if (pk != null && pk.Name.Equals(columnName))
                    {
                        return pk;
                    }
                }
            }
            return null;
        }

        public Dictionary<ColumnAttribute, object> GetAllColumnValues<T>(T obj)
        {
            Dictionary<ColumnAttribute, object> columns = new();
            var t = obj.GetType();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                var attributes = p.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    ColumnAttribute? c = attribute as ColumnAttribute;
                    if (c != null)
                    {
                        columns.Add(c, p.GetValue(obj));
                    }
                }
            }
            return columns;
        }

        public string GetTableName<T>()
        {
            var t = typeof(T);
            var tableAttributes = t.GetCustomAttributes(typeof(TableAttribute), true);
            if (tableAttributes[0] != null)
            {
                TableAttribute? tba = tableAttributes[0] as TableAttribute;
                return tba.Name;
            }
            return "";
        }

        public List<ColumnAttribute> GetColumns<T>()
        {
            Type t = typeof(T);
            PropertyInfo[] properties = t.GetProperties();
            var list = new List<ColumnAttribute>();
            foreach (PropertyInfo p in properties)
            {
                var attributes = p.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    ColumnAttribute? c = attribute as ColumnAttribute;
                    if (c != null)
                    {
                        list.Add(c);
                    }
                }
            }
            return list;
        }

        public List<KeyValuePair<ColumnAttribute, ForeignKeyAttribute>> GetAllForeignKeyAttributes<T>(string referenceTable)
        {
            var list = new List<KeyValuePair<ColumnAttribute, ForeignKeyAttribute>>();
            Type type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                ForeignKeyAttribute? foreignKey = null;
                ColumnAttribute? columnAttribute = null;
                foreach (var attribute in attributes)
                {
                    if (foreignKey == null) foreignKey = attribute as ForeignKeyAttribute;
                    if (columnAttribute == null) columnAttribute = attribute as ColumnAttribute;

                    if (foreignKey != null && columnAttribute != null && foreignKey.ReferenceTable.Equals(referenceTable))
                    {
                        list.Add(new KeyValuePair<ColumnAttribute, ForeignKeyAttribute>(columnAttribute, foreignKey));
                    }
                }
            }
            return list;
        }

        public List<ColumnAttribute> GetColumnAttributes<T>()
        {
            Type type = typeof(T);
            List<ColumnAttribute> list = new();
            foreach (var property in type.GetProperties())
            {
                var attributes = property.GetCustomAttributes(false);
                for (int i = 0; i < attributes.Length; i++)
                {
                    var attribute = attributes[i];
                    var column = attribute as ColumnAttribute;
                    if (column != null)
                        list.Add(column);
                }
            }
            return list;
        }
    }
}
