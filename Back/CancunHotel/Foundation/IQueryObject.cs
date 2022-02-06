namespace Foundation.Filter
{
    public interface IQueryObject
    {
        int? PageIndex { get; set; }
        int? PageSize { get; set; }
        string SortField { get; set; }
        bool? SortOrderAscending { get; set; }
    }
}
