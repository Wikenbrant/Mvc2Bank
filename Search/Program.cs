using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Search
{
    class Program
    {
        public static bool _run = true;

        static async Task Main()
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

            Console.CancelKeyPress += CancelHandler;

            var app = provider.GetRequiredService<Application>();

            while (_run)
            {
                await app.RunAsync().ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromHours(1)).ConfigureAwait(false);
            }
        }

        protected static void CancelHandler(object sender, ConsoleCancelEventArgs args)
        {
            _run = false;
        }
    }
}
