

namespace Wait.Shared;


public sealed class PagedList<T>
{
    
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }

}