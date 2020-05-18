using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.SearchModels;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using Search.Repository;
using IndexAction = Microsoft.Azure.Search.Models.IndexAction;
using IndexBatch = Microsoft.Azure.Search.Models.IndexBatch;

namespace Search
{
    public class Application
    {
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        public Application(IRepository repository,IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public async Task RunAsync()
        {
            var serviceClient = CreateSearchServiceClient(_configuration);
            var indexName = _configuration["SearchIndexName"];

            Console.WriteLine("{0}", "Deleting index...\n");
            DeleteIndexIfExists(indexName, serviceClient);

            Console.WriteLine("{0}", "Creating index...\n");
            CreateIndex(indexName, serviceClient);

            var indexClient = serviceClient.Indexes.GetClient(indexName);
            Console.WriteLine("{0}", "Uploading documents...\n");
            await UploadDocumentsAsync(indexClient).ConfigureAwait(false);

            Console.WriteLine("{0}", "Complete.  Press any key to end application...\n");
            Console.ReadKey();
        }

        private async Task UploadDocumentsAsync(ISearchIndexClient indexClient)
        {
            var customers = await _repository.GetCustomersAsync().ConfigureAwait(false);

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