using System.Threading.Tasks;
using Domain.Entities;
using Domain.SearchModels;

namespace Application.Common.Interfaces
{
    public interface ISearchService
    {
        Task<SearchData> RunQueryAsync(SearchData model, int page, int leftMostPage,
            string[] searchFields = null, string[] OrderBy = null, string[] selectFields = null);

        Task CreateOrUpdateCustomers(params Customer[] customers);

        Task DeleteCustomers(params Customer[] customers);
    }
}