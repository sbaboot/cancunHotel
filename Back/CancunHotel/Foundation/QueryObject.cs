namespace Foundation.Filter
{
    public abstract class QueryObject : IQueryObject
    {
        public QueryObject() { }
        public QueryObject(IQueryObject @object)
        {
            if (@object == null)
                return;

            this.PageIndex = @object.PageIndex;
            this.PageSize = @object.PageSize;
            this.SortField = @object.SortField;
            this.SortOrderAscending = @object.SortOrderAscending;
        }

        public int? PageIndex { get; set; } = null;
        public int? PageSize { get; set; } = null;
        public string SortField { get; set; } = null;
        public bool? SortOrderAscending { get; set; } = null;
    }
}
