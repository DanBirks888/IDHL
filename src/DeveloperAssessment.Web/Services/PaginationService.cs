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

        // Clamp the page number to ensure it is within valid bounds
        var currentPage = Math.Max(1, Math.Min(page, totalPages));

        // Calculate the items to skip and take for pagination
        var skipCount = (currentPage - 1) * pageSize;
        var paginatedItems = items.Skip(skipCount).Take(pageSize);

        // Return the populated pagination model
        return new PaginationModel<T>
        {
            PageSize = pageSize,
            PageIndex = currentPage,
            TotalPages = totalPages,
            Items = paginatedItems
        };
    }
}