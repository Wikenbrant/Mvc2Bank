using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.SearchModels;
using Microsoft.Azure.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IndexAction = Microsoft.Azure.Search.Models.IndexAction;
using IndexBatch = Microsoft.Azure.Search.Models.IndexBatch;

namespace Infrastructure.Search
{
    public class SearchIndexer : IApplicationDbContext
    {
        private readonly IApplicationDbContext _inner;
        private readonly IConfiguration _configuration;

        public SearchIndexer(IApplicationDbContext inner, IConfiguration configuration)
        {
            _inner = inner;
            _configuration = configuration;
        }
        public DbSet<Account> Accounts
        {
            get => _inner.Accounts;
            set => _inner.Accounts = value;
        }
        public DbSet<Card> Cards
        {
            get => _inner.Cards;
            set => _inner.Cards = value;
        }
        public DbSet<Customer> Customers
        {
            get => _inner.Customers;
            set => _inner.Customers = value;
        }
        public DbSet<Disposition> Dispositions
        {
            get => _inner.Dispositions;
            set => _inner.Dispositions = value;
        }
        public DbSet<Loan> Loans
        {
            get => _inner.Loans;
            set => _inner.Loans = value;
        }
        public DbSet<PermanentOrder> PermanentOrder
        {
            get => _inner.PermanentOrder;
            set => _inner.PermanentOrder = value;
        }

        public DbSet<Transaction> Transactions
        {
            get => _inner.Transactions;
            set => _inner.Transactions = value;
        }

        public DbSet<User> User
        {
            get => _inner.User;
            set => _inner.User = value;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            await RunAsync().ConfigureAwait(false);

            return await _inner.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task RunAsync()
        {
            var serviceClient = CreateSearchServiceClient(_configuration);
            var indexName = _configuration["SearchIndexName"];

            DeleteIndexIfExists(indexName, serviceClient);

            CreateIndex(indexName, serviceClient);

            var indexClient = serviceClient.Indexes.GetClient(indexName);

            await UploadDocumentsAsync(indexClient).ConfigureAwait(false);
        }

        private async Task UploadDocumentsAsync(ISearchIndexClient indexClient)
        {
            var customers = await _inner.Customers
                .Select(c=>new CustomerSearch
                {
                    CustomerId = c.CustomerId.ToString(),
                    Country = c.Country,
                    Surname = c.Surname,
                    Givenname = c.Givenname,
                    City = c.City,
                    Zipcode = c.Zipcode,
                    Telephonenumber = c.Telephonenumber,
                    Emailaddress = c.Emailaddress,
                    Streetaddress = c.Streetaddress,
                    CountryCode = c.CountryCode,
                    Gender = c.Gender,
                    NationalId = c.NationalId,
                    Telephonecountrycode = c.Telephonecountrycode
                })
                .ToListAsync()
                .ConfigureAwait(false);

            var actions = customers.Select(IndexAction.Upload);

            var batch = IndexBatch.New(actions);


            try
            {
                indexClient.Documents.Index(batch);
            }
            catch (IndexBatchException e)
            {
                // When a service is under load, indexing might fail for some documents in the batch. 
                // Depending on your application, you can compensate by delaying and retrying. 
                // For this simple demo, we just log the failed document keys and continue.
                Console.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }

            // Wait 2 seconds before starting queries 
            Console.WriteLine("Waiting for indexing...\n");
            Thread.Sleep(2000);
        }

        private SearchServiceClient CreateSearchServiceClient(IConfiguration configuration)
        {
            var searchServiceName = configuration["SearchServiceName"];
            var adminApiKey = configuration["SearchServiceAdminApiKey"];

            return new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
        }

        // Delete an existing index to reuse its name
        private void DeleteIndexIfExists(string indexName, ISearchServiceClient serviceClient)
        {
            if (serviceClient.Indexes.Exists(indexName))
            {
                serviceClient.Indexes.Delete(indexName);
            }
        }

        // Create an index whose fields correspond to the properties of the Hotel class.
        // The Address property of Hotel will be modeled as a complex field.
        // The properties of the Address class in turn correspond to sub-fields of the Address complex field.
        // The fields of the index are defined by calling the FieldBuilder.BuildForType() method.
        private void CreateIndex(string indexName, ISearchServiceClient serviceClient)
        {
            var definition = new Microsoft.Azure.Search.Models.Index
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<CustomerSearch>()
            };

            serviceClient.Indexes.Create(definition);
        }
    }
}