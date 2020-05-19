using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.SearchModels;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Search
{
    public static class GlobalVariables
    {
        public static int ResultsPerPage => 50;
        public static int MaxPageRange => 5;

        public static int PageRangeDelta => 2;

        public static string Contains = "~10";
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
            _indexClient = serviceClient.Indexes.GetClient(configuration["SearchIndexName"]);
        }

        public async Task<SearchData> RunQueryAsync(SearchData model, int page, int leftMostPage,
            string[] searchFields = null, string[] OrderBy = null, string[] selectFields = null)
        {
            var parameters = new SearchParameters
            {
                Top = GlobalVariables.ResultsPerPage,
                Skip = (page - 1) * GlobalVariables.ResultsPerPage,
                SearchMode = SearchMode.Any,
                SearchFields = searchFields,
                Select = selectFields,
                OrderBy = OrderBy,
                IncludeTotalResultCount = true,
            };

            var searchText = String.IsNullOrEmpty(model.SearchText) ? "*" : model.SearchText + GlobalVariables.Contains;

            // For efficiency, the search call should be asynchronous, so use SearchAsync rather than Search.
            model.ResultList = await _indexClient.Documents
                .SearchAsync<CustomerSearch>(searchText, parameters)
                .ConfigureAwait(false);

            // This variable communicates the total number of pages to the view.
            model.PageCount = ((int) model.ResultList.Count + GlobalVariables.ResultsPerPage) /
                              GlobalVariables.ResultsPerPage;

            // This variable communicates the page number being displayed to the view.
            model.CurrentPage = page;

            if (page == 1)
            {
                leftMostPage = 1;
            }
            else if (page <= leftMostPage)
            {
                // Trigger a switch to a lower page range.
                leftMostPage = Math.Max(page - GlobalVariables.PageRangeDelta, 1);
            }
            else if (page >= leftMostPage + GlobalVariables.MaxPageRange - 1)
            {
                // Trigger a switch to a higher page range.
                leftMostPage = Math.Min(page - GlobalVariables.PageRangeDelta,
                    model.PageCount - GlobalVariables.MaxPageRange);
            }

            model.LeftMostPage = leftMostPage;

            // Calculate the number of page numbers to display.
            model.PageRange = Math.Min(model.PageCount - leftMostPage, GlobalVariables.MaxPageRange);

            return model;
        }
    }
}