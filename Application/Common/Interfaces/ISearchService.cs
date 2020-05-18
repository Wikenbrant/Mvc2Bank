using System.Threading.Tasks;
using Domain.SearchModels;

namespace Application.Common.Interfaces
{
    public interface ISearchService
    {
        Task<SearchData> RunQueryAsync(SearchData model, int page, int leftMostPage,
            string[] searchFields = null, string[] OrderBy = null, string[] selectFields = null);
    }
}