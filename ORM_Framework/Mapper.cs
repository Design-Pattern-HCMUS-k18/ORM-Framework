using ORM_Framework.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework
{
    public abstract class Mapper
    {
        public object? GetFirst(IEnumerable source)
        {
            IEnumerator iter = source.GetEnumerator();

            if (iter.MoveNext())
            {
                return iter.Current;
            }
            return null;
        }
        public T MapRowAndRelationship<T>(DBConnection cnn, DataRow dr)
        {
            T obj = (T)Activator.CreateInstance(typeof(T));
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                ColumnAttribute? columnMapping = null;
                foreach (var attribute in attributes)
                {
                    var column = attribute as ColumnAttribute;
                    if (column != null)
                    {
                        columnMapping = column;
                        break;
                    }
                }

                if (columnMapping != null)
                {
                    try
                    {
                        property.SetValue(obj, dr[columnMapping.Name]);
                    } catch (Exception)
                    {
                        continue;
                    }
                }
            }

            MapOneToMany(cnn, dr, obj);
            MapOneToOne(cnn, dr, obj);
            MapManyToOne(cnn, dr, obj);

            return obj;
        }

        public abstract void MapOneToOne<T>(DBConnection cnn, DataRow dr, T obj);
        public abstract void MapOneToMany<T>(DBConnection cnn, DataRow dr, T obj);
        public abstract void MapManyToOne<T>(DBConnection cnn, DataRow dr, T obj);
    }
}
