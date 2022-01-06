using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ORM_Framework.Attributes;

namespace ORM_Framework.SQL
{
    public class SqlSelect<T> : SqlQuery, IQueryBuilder<T>
    {
        private SqlSelect(SqlConnection cnn) : base(cnn)
        {
            SqlMapper mapper = new();

            _query += "SELECT";

            foreach (ColumnAttribute column in mapper.GetColumns<T>())
                _query = string.Format("{0} {1},", _query, column.Name);


            _query = _query.Substring(0, _query.Length - 1);
            _query = string.Format("{0} FROM {1}", _query, mapper.GetTableName<T>());
        }

        public static IQueryBuilder<T> Create(SqlConnection cnn)
        {
            return new SqlSelect<T>(cnn);
        }

        public IQueryBuilder<T> Where(string condition)
        {
            _query = string.Format("{0} WHERE {1}", _query, condition);
            return this;
        }

        public IQueryBuilder<T> Having(string condition)
        {
            _query = string.Format("{0} HAVING {1}", _query, condition);
            return this;
        }

        public IQueryBuilder<T> GroupBy(string columnNames)
        {
            _query = string.Format("{0} GROUP BY {1}", _query, columnNames);
            return this;
        }

        public List<T> Run()
        {
            return ExecuteQueryWithoutRelationship<T>();
        }
    }
}