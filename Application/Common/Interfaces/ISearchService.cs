using System.Threading.Tasks;
using Domain.SearchModels;

namespace Application.Common.Interfaces
{
    public interface ISearchService
    {
        Task<SearchData> RunQueryAsync(string searchText, int page, int pageSize, params string[] selectFields);
    }
}