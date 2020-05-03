using System.Threading.Tasks;
using NUnit.Framework;

namespace Application.Test
{
    using static Testing;
    public abstract class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
           await  ResetStateAsync();
        }
    }
}