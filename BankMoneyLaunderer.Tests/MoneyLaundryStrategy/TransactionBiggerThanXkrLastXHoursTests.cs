using System.IO;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using BankMoneyLaunderer.MoneyLaundryRepository;
using BankMoneyLaunderer.MoneyLaundryStrategy;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace Application.Test.MoneyLaundryStrategy
{
    public class TransactionBiggerThanXkrLastXHoursTests
    {
        public Mock<IRepository> Repository { get; set; }
        public IMoneyLaundryConfig MoneyLaundryConfig { get; set; }

        [SetUp]
        public void SetUp()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            MoneyLaundryConfig = configuration.GetSection(nameof(MoneyLaundryConfig)).Get<MoneyLaundryConfig>();

            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            Repository = new Mock<IRepository>();
        }

        [Test]
        public Task Should()
        {
            var strategy = new TransactionBiggerThanXkrLastXHours(MoneyLaundryConfig,Repository.Object);

            //var report = strategy.GenerateReportAsync()
            return Task.CompletedTask;
        }

    }
}