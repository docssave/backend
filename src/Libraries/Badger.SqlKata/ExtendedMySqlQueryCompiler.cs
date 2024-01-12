using SqlKata;
using SqlKata.Compilers;
using System.Text;

namespace Badger.SqlKata
{
    public sealed class ExtendedMySqlQueryCompiler : MySqlCompiler
    {
        protected override SqlResult CompileRaw(Query query)
        {
            if (query.Method == "upsert")
            {
                return CompileUpsertQuery(query);
            }

            return base.CompileRaw(query);
        }
        private SqlResult CompileUpsertQuery(Query query)
        {
            if (!query.HasComponent("from", EngineCode))
            {
                throw new InvalidOperationException("No table set to upsert");
            }

            if (query.GetOneComponent<AbstractFrom>("from", EngineCode) is not FromClause fromClause)
            {
                throw new InvalidOperationException("Invalid table expression");
            }

            var tableName = Wrap(fromClause.Table);
            var sqlResult = new SqlResult
            {
                Query = query,
            };

            UpdateRawSql(query.GetOneComponent<UpsertClause>("upsert", EngineCode));

            return sqlResult;

            void UpdateRawSql(UpsertClause upsertClause)
            {
                var inserColumns = GetInsertColumnsList(upsertClause.InsertColumns);
                var insertValues = Parameterize(sqlResult, upsertClause.InsertValues);
                var updateColumnsValue = GetUpdateColumnsValue(upsertClause.UpdateColumns, upsertClause.UpdateValues);

                var rawSql = new StringBuilder()
                    .Append(SingleInsertStartClause)
                    .Append($" {tableName} ")
                    .AppendLine($" {inserColumns} ")
                    .AppendLine($"VALUES ({insertValues})")
                    .AppendLine("ON DUPLICATE KEY UPDATE")
                    .Append(updateColumnsValue)
                    .Append(';');

                sqlResult.RawSql = rawSql.ToString();
            }

            string GetUpdateColumnsValue(IReadOnlyList<string> columns, IReadOnlyList<object> values)
            {
                var columnsValue = new List<string>();

                for (int i = 0; i < columns.Count; i++)
                {
                    var column = columns[i];
                    var value = values[i];

                    columnsValue.Add($"{Wrap(column)} = {Parameter(sqlResult, value)}");
                }

                return string.Join(", ", columnsValue);
            }
        }
    }
}
