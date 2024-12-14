using DeveloperAssessment.Web.DomainModels.Pagination;

namespace DeveloperAssessment.Web.Interfaces;

public interface IPaginationService
{
    PaginationModel<T> Paginate<T>(List<T> items, int page, int pageSize);
}