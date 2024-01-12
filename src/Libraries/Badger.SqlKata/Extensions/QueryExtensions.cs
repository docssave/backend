using SqlKata;

namespace Badger.SqlKata.Extensions
{
    public static class QueryExtensions
    {
        public static Query AsUpsert(this Query query, IEnumerable<KeyValuePair<string, object>> insertValues, IEnumerable<KeyValuePair<string, object>> updateValues)
        {
            if (!insertValues.Any() || !updateValues.Any())
            {
                throw new InvalidOperationException($"{nameof(insertValues)} or {nameof(updateValues)} cannot be null or empty");
            }

            query.Method = "upsert";

            query.ClearComponent("upsert").AddComponent("upsert", new UpsertClause
            {
                InsertColumns = insertValues.Select(value => value.Key).ToList(),
                InsertValues = insertValues.Select(value => value.Value).ToList(),
                UpdateColumns = updateValues.Select(value => value.Key).ToList(),
                UpdateValues = updateValues.Select(value => value.Value).ToList(),
            });

            return query;
        }
    }
}
