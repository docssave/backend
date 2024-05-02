using SqlKata;

namespace Badger.SqlKata
{
    internal sealed class UpsertClause : AbstractClause
    {
        public List<string> InsertColumns { get; set; }

        public List<object> InsertValues { get; set; }

        public List<string> UpdateColumns { get; set; }

        public List<object> UpdateValues { get; set; }

        public override AbstractClause Clone() => new UpsertClause
        {
            Component = this.Component,
            Engine = this.Engine,
            InsertColumns = this.InsertColumns,
            InsertValues = this.InsertValues,
            UpdateColumns = this.UpdateColumns,
            UpdateValues = this.UpdateValues,
        };
    }
}
