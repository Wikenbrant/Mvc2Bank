using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.SearchModels;

namespace BankMoneyLaunderer.Services
{
    public class FakeSearchService:ISearchService
    {
        public Task<SearchData> RunQueryAsync(SearchData model, int page, int leftMostPage, string[] searchFields = null, string[] OrderBy = null,
            string[] selectFields = null)
        {
            return Task.FromResult(new SearchData());
        }

        public Task CreateOrUpdateCustomers(params Customer[] customers)
        {
            return Task.CompletedTask;
        }

        public Task DeleteCustomers(params Customer[] customers)
        {
            return Task.CompletedTask;
        }
    }
}
