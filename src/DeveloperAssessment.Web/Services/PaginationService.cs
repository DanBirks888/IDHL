using DeveloperAssessment.Web.DomainModels.Pagination;
using DeveloperAssessment.Web.Interfaces;

namespace DeveloperAssessment.Web.Services;

public class PaginationService : IPaginationService
{
    public PaginationModel<T> Paginate<T>(List<T> items, int page, int pageSize)
    {
        // Using concrete list instead of Ienumerable for this
        var totalItems = items.Count;

        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        // Ensures currentPage is within the valid range of 1 to totalPages.
        var currentPage = Math.Max(1, Math.Min(page, totalPages));

        var skipCount = (currentPage - 1) * pageSize;
        var paginatedItems = items.Skip(skipCount).Take(pageSize);

        return new PaginationModel<T>
        {
            PageSize = pageSize,
            PageIndex = currentPage,
            TotalPages = totalPages,
            Items = paginatedItems
        };
    }
}