using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.Azure.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using Microsoft.Azure.Search.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.SearchModels;

namespace Search
{
    class Program
    {
        public static DesignTimeDbContextFactory Factory { get; set; }
        public static IMapper Mapper { get; set; }

        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Factory = new DesignTimeDbContextFactory();

            Mapper = InitializeAutomapper();

            var serviceClient = CreateSearchServiceClient(configuration);
            var indexName = configuration["SearchIndexName"];

            Console.WriteLine("{0}", "Deleting index...\n");
            DeleteIndexIfExists(indexName, serviceClient);

            Console.WriteLine("{0}", "Creating index...\n");
            CreateIndex(indexName, serviceClient);

            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient(indexName);
            Console.WriteLine("{0}", "Uploading documents...\n");
            await UploadDocumentsAsync(indexClient).ConfigureAwait(false);

            Console.WriteLine("{0}", "Complete.  Press any key to end application...\n");
            Console.ReadKey();
        }

        private static SearchServiceClient CreateSearchServiceClient(IConfiguration configuration)
        {
            var searchServiceName = configuration["SearchServiceName"];
            var adminApiKey = configuration["SearchServiceAdminApiKey"];

            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            return serviceClient;
        }

        // Delete an existing index to reuse its name
        private static void DeleteIndexIfExists(string indexName, ISearchServiceClient serviceClient)
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
        private static void CreateIndex(string indexName, ISearchServiceClient serviceClient)
        {
            var definition = new Microsoft.Azure.Search.Models.Index
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<CustomerSearch>()
            };

            serviceClient.Indexes.Create(definition);
        }

        private static async Task UploadDocumentsAsync(ISearchIndexClient indexClient)
        {

            await using var context = Factory.CreateDbContext(new []{""});
            var customers = await context.Customers
                .ProjectTo<CustomerSearch>(Mapper.ConfigurationProvider)
                .ToListAsync().ConfigureAwait(false);

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

        static IMapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerSearch>()
                    .ForMember(s => s.CustomerId,
                        map => map.MapFrom(s => s.CustomerId.ToString()))
                    .ForMember(s => s.Dispositions,
                        map => map.MapFrom(s => s.Dispositions));

                cfg.CreateMap<Disposition, DispositionSearch>()
                    .ForMember(s => s.Account,
                        map => map.MapFrom(s => s.Account));

                cfg.CreateMap<Account, AccountSearch>()
                    .ForMember(s => s.Balance,
                        map => map.MapFrom(s => Convert.ToDouble(s.Balance)));
            });

            return config.CreateMapper();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = configuration.GetConnectionString("Default");

            builder.UseSqlServer(connectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}
