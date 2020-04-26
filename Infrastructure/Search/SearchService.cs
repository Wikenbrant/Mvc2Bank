using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.SearchModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Search
{
    public static class GlobalVariables
    {
    }
    public class SearchService : ISearchService
    {
        private static ISearchIndexClient _indexClient;

        public SearchService(IConfiguration configuration)
        {

            // Pull the values from the appsettings.json file.
            var searchServiceName = configuration["SearchServiceName"];
            var queryApiKey = configuration["SearchServiceQueryApiKey"];

            // Create a service and index client.
            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(queryApiKey));
            _indexClient = serviceClient.Indexes.GetClient(configuration["SearchServiceName"]);
        }

        public async Task<SearchData> RunQueryAsync(string searchText, int page, int pageSize, string[] selectFields = null)
        {
            var data = new SearchData();
            var parameters = new SearchParameters
            {
                // Enter Hotel property names into this list so only these values will be returned.
                // If Select is empty, all values will be returned, which can be inefficient.
                Select = selectFields,

                SearchMode = SearchMode.All,

                // Skip past results that have already been returned.
                Skip = (page-1) * pageSize,

                // Take only the next page worth of results.
                Top = pageSize,

                // Include the total number of results.
                IncludeTotalResultCount = true,
            };

            // For efficiency, the search call should be asynchronous, so use SearchAsync rather than Search.
            data.ResultList = await _indexClient.Documents.SearchAsync<CustomerSearch>(searchText, parameters).ConfigureAwait(false);

            // This variable communicates the total number of pages to the view.
            data.PageCount = (int)Math.Ceiling((double)(data.ResultList.Count ?? 1));

            // This variable communicates the page number being displayed to the view.
            data.CurrentPage = page;

            return data;
        }
    }
}