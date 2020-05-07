using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Customers.Queries.GetCustomer;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.ViewComponents.CustomerDetail;

namespace Web.Tests
{
    public class CustomerControllerTests
    {
        private Mock<IMediator> _mediator;
        private CustomersController _sut;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _sut = new CustomersController();
        }

        [Test]
        public async Task Index_should_return_correct_viewmodel()
        {

            var viewmodel = new CustomerViewModel{Customer = new CustomerDto{CustomerId = 1}};
            _mediator.Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), default))
                .Returns(Task.FromResult(viewmodel));

            var query = new GetCustomerQuery{Id = 1};

            var result = _sut.Index(query) as ViewComponentResult;

            result.Should().NotBeNull();

            var argument = result.Arguments.GetType().GetProperties().FirstOrDefault(p => p.Name == "query").GetValue(result.Arguments) as GetCustomerQuery;

            argument.Should().NotBeNull().And.BeOfType<GetCustomerQuery>();

            argument.Id.Should().Be(query.Id);

            result.ViewComponentName.Should().Be(nameof(ViewComponents.CustomerDetail));

            var viewComponent = new CustomerDetail(_mediator.Object);

            var viewComponentResult = await viewComponent.InvokeAsync(query) as ViewViewComponentResult;

            ((CustomerViewModel) viewComponentResult.ViewData.Model).Customer.CustomerId.Should().Be(viewmodel.Customer.CustomerId);

            _mediator.Verify(x=>x.Send(It.Is<GetCustomerQuery>(q=>q.Id == query.Id),default));
        }
    }
}