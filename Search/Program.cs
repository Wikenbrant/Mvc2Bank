using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Search
{
    class Program
    {

        static Task Main()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var collection = new ServiceCollection();
            collection.AddSingleton<IConfiguration>(configuration);
            collection.AddSingleton<Application>();
            var startup = new Startup(configuration);
            startup.ConfigureServices(collection);

            var provider = collection.BuildServiceProvider();

            var app = provider.GetRequiredService<Application>();

            return app.RunAsync();
        }
    }
}
