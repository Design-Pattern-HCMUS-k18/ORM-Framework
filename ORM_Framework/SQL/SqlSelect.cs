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
        private string TableName;
        private List<string> SelectStatements = new List<string>();
        private List<string> WhereConditions = new List<string>();
        private List<string> HavingConditions = new List<string>();
        private List<string> GroupByColumnNames = new List<string>();

        public SqlSelect(SqlConnection cnn, string[] statements) : base(cnn)
        {
            SqlMapper mapper = new();

            if (statements?.Length != 0)
            {
                foreach (string statement in statements)
                    SelectStatements.Add(statement);
            }
            else
            {
                foreach (ColumnAttribute column in mapper.GetColumns<T>())
                    SelectStatements.Add(column.Name);
            }

            TableName = mapper.GetTableName<T>();
        }

        public static IQueryBuilder<T> Create(SqlConnection cnn, string[] statements)
        {
            return new SqlSelect<T>(cnn, statements);
        }

        public IQueryBuilder<T> Where(string firstCondition, params string[] conditions)
        {
            ConditionalQuery("WHERE", "AND", firstCondition, conditions);

            return this;
        }

        public IQueryBuilder<T> WhereOr(string firstCondition, params string[] conditions)
        {
            ConditionalQuery("WHERE", "OR", firstCondition, conditions);

            return this;
        }

        public IQueryBuilder<T> Having(string firstCondition, params string[] conditions)
        {
            ConditionalQuery("HAVING", "AND", firstCondition, conditions);

            return this;
        }

        public IQueryBuilder<T> HavingOr(string firstCondition, params string[] conditions)
        {
            ConditionalQuery("HAVING", "OR", firstCondition, conditions);

            return this;
        }

        public IQueryBuilder<T> GroupBy(string firstColumnName, params string[] columnNames)
        {
            GroupByColumnNames.Add(firstColumnName);

            if (columnNames.Length > 0)
            {
                foreach (string columnName in columnNames)
                    GroupByColumnNames.Add(columnName);
            }

            return this;
        }

        public List<T> Run()
        {
            try
            {
                BuildQuery();

                Console.WriteLine("Raw query: {0}", _query);
                return ExecuteQuery<T>();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        private void ConditionalQuery(string type, string operation, string firstCondition, params string[] conditions)
        {
            List<string> list = null;
            if (type == "WHERE") list = WhereConditions;
            if (type == "HAVING") list = HavingConditions;

            if (list.Count > 0)
                list.Add(operation);

            list.Add(firstCondition);

            if (conditions?.Length > 0)
            {
                list.Add(operation);
                for (int i = 0; i < conditions.Length; i++)
                {
                    list.Add(conditions[i]);
                    if (i != conditions.Length - 1)
                        list.Add(operation);
                }
            }

            if (type == "WHERE") WhereConditions = list;
            if (type == "HAVING") HavingConditions = list;
        }

        private void BuildQuery()
        {
            // Build select
            _query = "SELECT";

            foreach (string statement in SelectStatements)
                _query = string.Format("{0} {1},", _query, statement);
            _query = _query.Substring(0, _query.Length - 1);

            _query = string.Format("{0} FROM {1}", _query, TableName);

            // Build where
            if (WhereConditions.Count > 0)
            {
                _query = string.Format("{0} WHERE", _query);
                foreach (string condition in WhereConditions)
                    _query = string.Format("{0} {1}", _query, condition);
            }
            
            // Build group by
            if (GroupByColumnNames.Count > 0)
            {
                _query = string.Format("{0} GROUP BY", _query);
                foreach (string columnName in GroupByColumnNames)
                    _query = string.Format("{0} {1},", _query, columnName);
                _query = _query.Substring(0, _query.Length - 1);
            }

            // Build having
            if (HavingConditions.Count > 0)
            {
                _query = string.Format("{0} HAVING", _query);
                foreach (string condition in HavingConditions)
                    _query = string.Format("{0} {1}", _query, condition);
            }
        }
    }
}