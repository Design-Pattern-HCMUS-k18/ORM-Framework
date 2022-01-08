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

                        List<ForeignKeyAttribute> foreignKeys = mapper.GetAllForeignKeyAttributes<T>(onetoone.ReferenceTable);

                        MethodInfo getColumnAttribute = mapper.GetType().GetMethod("GetColumnAttributes")
                            .MakeGenericMethod(new Type[] { ptype });
                        List<ColumnAttribute> columnAttributes = getColumnAttribute.Invoke(mapper, null) as List<ColumnAttribute>;

                        foreach(var foreignKey in foreignKeys)
                        {
                            ColumnAttribute column = null;
                            foreach (var columnAttribute in columnAttributes)
                            {
                                if(foreignKey.ReferenceColumn.Equals(columnAttribute.Name))
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

                                whereStr += string.Format(format, foreignKey.ReferenceTable, dr[foreignKey.ReferenceColumn]);
                            }
                        }
                        if (!string.IsNullOrEmpty(whereStr))
                        {
                            whereStr = whereStr.Substring(0, whereStr.Length - 2);
                            string query = string.Format("SELECT * FROM {0} WHERE {1}", onetoone.ReferenceTable, whereStr);

                            cnn.Open();
                            MethodInfo method = cnn.GetType().GetMethod("ExecuteQueryWithOutRelationship")
                            .MakeGenericMethod(new Type[] { type });
                            var ienumerable = (IEnumerable)method.Invoke(cnn, new object[] { query });
                            cnn.Close();

                            MethodInfo method2 = mapper.GetType().GetMethod("GetFirst");
                            var firstElement = method2.Invoke(mapper, new object[] { ienumerable });

                            property.SetValue(obj, firstElement);
                        }
                    }
                }
            }
        }
    }
}
