using System.Collections;

namespace DeveloperAssessment.Web.DomainModels.Pagination;

public class PaginationModel<T>
{
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int PageIndex { get; set; }
    public IEnumerable? Items { get; set; }
}