using SqlKata;

namespace Badger.SqlKata.Extensions
{
    public static class QueryExtensions
    {
        public static Query AsUpsert(this Query query, IEnumerable<KeyValuePair<string, object>> insertValues, IEnumerable<KeyValuePair<string, object>> updateValues)
        {
            var insert = insertValues.ToList();
            var update = updateValues.ToList();

            if (insert.Count == 0 || update.Count == 0)
            {
                throw new InvalidOperationException($"{nameof(insertValues)} or {nameof(update)} cannot be null or empty");
            }

            query.Method = "upsert";

            query.ClearComponent("upsert").AddComponent("upsert", new UpsertClause
            {
                InsertColumns = insert.Select(value => value.Key).ToList(),
                InsertValues = insert.Select(value => value.Value).ToList(),
                UpdateColumns = update.Select(value => value.Key).ToList(),
                UpdateValues = update.Select(value => value.Value).ToList(),
            });

            return query;
        }
    }
}
