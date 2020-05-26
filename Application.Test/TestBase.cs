using System.Threading.Tasks;
using NUnit.Framework;

namespace Application.Test
{
    using static Testing;
    public abstract class TestBase
    {
        [SetUp]
        public Task TestSetUp()
        {
           return ResetStateAsync();
        }
    }
}