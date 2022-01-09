using ORM_Framework.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Framework.SQL
{
    public class SqlMapper : Mapper
    {
        public SqlMapper()
        {
            
        }

        public override void MapOneToOne<T>(DBConnection cnn, DataRow dr, T obj)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            foreach(var property in properties)
            {
                Type ptype = property.PropertyType;
                var attributes = property.GetCustomAttributes(false);
                List<OneToOneAttribute> oneToOneAttributes = new List<OneToOneAttribute>();
                foreach(var attribute in attributes)
                {
                    var onetoone = attribute as OneToOneAttribute;
                    if(onetoone != null)
                    {
                        oneToOneAttributes.Add(onetoone);
                    }
                }

                if(oneToOneAttributes != null && oneToOneAttributes.Count > 0)
                {
                    foreach(var onetoone in oneToOneAttributes)
                    {
                        SqlMapper mapper = new SqlMapper();
                        string whereStr = string.Empty;

                        var foreignKeys = mapper.GetAllForeignKeyAttributes<T>(onetoone.ReferenceTable);

                        MethodInfo getColumnAttribute = mapper.GetType().GetMethod("GetColumnAttributes")
                            .MakeGenericMethod(new Type[] { ptype });
                        List<ColumnAttribute> columnAttributes = getColumnAttribute.Invoke(mapper, null) as List<ColumnAttribute>;

                        foreach(var foreignKey in foreignKeys)
                        {
                            ColumnAttribute column = null;
                            foreach (var columnAttribute in columnAttributes)
                            {
                                if(foreignKey.Value.ReferenceColumn.Equals(columnAttribute.Name))
                                {
                                    column = columnAttribute;
                                    break;
                                }
                            }

                            if(column != null)
                            {
                                string format = "{0} = {1}, ";
                                if (column.Type == DataType.NCHAR || column.Type == DataType.NVARCHAR)
                                    format = "{0} = N'{1}', ";
                                else if (column.Type == DataType.CHAR || column.Type == DataType.VARCHAR)
                                    format = "{0} = '{1}', ";

                                whereStr += string.Format(format, foreignKey.Value.ReferenceColumn, dr[foreignKey.Key.Name]);
                            }
                        }
                        if (!string.IsNullOrEmpty(whereStr))
                        {
                            whereStr = whereStr.Substring(0, whereStr.Length - 2);
                            string query = string.Format("SELECT * FROM {0} WHERE {1}", onetoone.ReferenceTable, whereStr);
                            Console.WriteLine(query);
                            MethodInfo method = cnn.GetType().GetMethod("ExecuteQueryWithoutRelationship")
                            .MakeGenericMethod(new Type[] { ptype });
                            var ienumerable = (IEnumerable)method.Invoke(cnn, new object[] { query });

                            MethodInfo method2 = mapper.GetType().GetMethod("GetFirst");
                            var firstElement = method2.Invoke(mapper, new object[] { ienumerable });

                            property.SetValue(obj, firstElement);
                        }
                    }
                }
            }
        }
        public override void MapManyToOne<T>(DBConnection cnn, DataRow dr, T obj)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                Type ptype = property.PropertyType;
                var attributes = property.GetCustomAttributes(false);
                var manyToOneAttributes = new List<ManyToOneAttribute>();
                foreach (var attribute in attributes)
                {
                    var manytoone = attribute as ManyToOneAttribute;
                    if (manytoone != null)
                    {
                        manyToOneAttributes.Add(manytoone);
                    }
                }

                if (manyToOneAttributes != null && manyToOneAttributes.Count > 0)
                {
                    foreach (var onetoone in manyToOneAttributes)
                    {
                        var mapper = new SqlMapper();
                        string whereStr = string.Empty;

                        var foreignKeys = mapper.GetAllForeignKeyAttributes<T>(onetoone.ReferenceTable);

                        MethodInfo getColumnAttribute = mapper.GetType().GetMethod("GetColumnAttributes")
                            .MakeGenericMethod(new Type[] { ptype });
                        var columnAttributes = getColumnAttribute.Invoke(mapper, null) as List<ColumnAttribute>;

                        foreach (var foreignKey in foreignKeys)
                        {
                            ColumnAttribute column = null;
                            foreach (var columnAttribute in columnAttributes)
                            {
                                if (foreignKey.Value.ReferenceColumn.Equals(columnAttribute.Name))
                                {
                                    column = columnAttribute;
                                    break;
                                }
                            }

                            if (column != null)
                            {
                                string format = "{0} = {1}, ";
                                if (column.Type == DataType.NCHAR || column.Type == DataType.NVARCHAR)
                                    format = "{0} = N'{1}', ";
                                else if (column.Type == DataType.CHAR || column.Type == DataType.VARCHAR)
                                    format = "{0} = '{1}', ";

                                whereStr += string.Format(format, foreignKey.Value.ReferenceColumn, dr[foreignKey.Key.Name]);
                            }
                        }
                        if (!string.IsNullOrEmpty(whereStr))
                        {
                            whereStr = whereStr.Substring(0, whereStr.Length - 2);
                            string query = string.Format("SELECT * FROM {0} WHERE {1}", onetoone.ReferenceTable, whereStr);
                            Console.WriteLine(query);
                            var method = cnn.GetType().GetMethod("ExecuteQueryWithoutRelationship")
                            .MakeGenericMethod(new Type[] { ptype });
                            var ienumerable = (IEnumerable)method.Invoke(cnn, new object[] { query });

                            var method2 = mapper.GetType().GetMethod("GetFirst");
                            var firstElement = method2.Invoke(mapper, new object[] { ienumerable });

                            property.SetValue(obj, firstElement);
                        }
                    }
                }
            }
        }
        public override void MapOneToMany<T>(DBConnection cnn, DataRow dr, T obj)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();
            foreach(var property in properties)
            {
                var ptype = property.PropertyType;
                var attributes = property.GetCustomAttributes(false);
                var oneToManyAttributes = new List<OneToManyAttribute>();
                foreach (var attribute in attributes)
                {
                    var onetomany = attribute as OneToManyAttribute;
                    if (onetomany != null)
                    {
                        oneToManyAttributes.Add(onetomany);
                    }
                }
                if (oneToManyAttributes.Count > 0)
                {
                    var genericType = ptype.GetGenericArguments().Single();
                    foreach(var onetomany in oneToManyAttributes)
                    {
                        SqlMapper mapper = new SqlMapper();
                        string whereStr = string.Empty;
                        var tableName = mapper.GetTableName<T>();
                        var getColumnMethod = mapper.GetType().GetMethod("GetAllForeignKeyAttributes")
                            .MakeGenericMethod(new Type[] { genericType });
                        var foreignKeys = (List<KeyValuePair<ColumnAttribute, ForeignKeyAttribute>>)getColumnMethod.Invoke(mapper, new object[] { tableName });

                        var columnAttributes = mapper.GetColumnAttributes<T>();
                        foreach (var foreignKey in foreignKeys)
                        {
                            ColumnAttribute column = null;
                            foreach (var columnAttribute in columnAttributes)
                            {
                                if (foreignKey.Value.ReferenceColumn.Equals(columnAttribute.Name))
                                {
                                    column = columnAttribute;
                                    break;
                                }
                            }
                            if (column != null)
                            {
                                string format = "{0} = {1}, ";
                                if (column.Type == DataType.NCHAR || column.Type == DataType.NVARCHAR)
                                    format = "{0} = N'{1}', ";
                                else if (column.Type == DataType.CHAR || column.Type == DataType.VARCHAR)
                                    format = "{0} = '{1}', ";
                                var primaryKey = mapper.GetPrimaryKey<T>(foreignKey.Value.ReferenceColumn);
                                if (primaryKey == null) continue;
                                whereStr += string.Format(format, foreignKey.Key.Name, dr[primaryKey.Name]);
                            }
                        }
                        if (!string.IsNullOrEmpty(whereStr))
                        {
                            whereStr = whereStr.Substring(0, whereStr.Length - 2);
                            string query = string.Format("SELECT * FROM {0} WHERE {1}", onetomany.ReferenceTable, whereStr);
                            Console.WriteLine(query);
                            MethodInfo method = cnn.GetType().GetMethod("ExecuteQueryWithoutRelationship")
                            .MakeGenericMethod(new Type[] { genericType });
                            var ienumerable = (IEnumerable)method.Invoke(cnn, new object[] { query });

                            property.SetValue(obj, ienumerable);
                        }
                    }
                }
            }
        }
    }
}
