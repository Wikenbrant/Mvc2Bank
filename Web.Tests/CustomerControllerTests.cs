using System.Linq;
using System.Threading.Tasks;
using Application.Customers.Queries.GetCustomer;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        private Mock<IMapper> _mapper;
        private CustomersController _sut;

        [SetUp]
        public void Setup()
        {
            _mediator = new Mock<IMediator>();
            _mapper = new Mock<IMapper>();
            _sut = new CustomersController(_mediator.Object, _mapper.Object);
        }

        [Test]
        public void Index_should_call_correct_view_component_with_correct_parameters()
        {
            var query = new GetCustomerQuery{Id = 1};

            var result = _sut.Index(query) as ViewComponentResult;

            result.Should().NotBeNull().And.BeOfType<ViewComponentResult>();

            var argument = result.Arguments.GetType().GetProperties().FirstOrDefault(p => p.Name == "query").GetValue(result.Arguments) as GetCustomerQuery;

            argument.Should().NotBeNull().And.BeOfType<GetCustomerQuery>();

            argument.Id.Should().Be(query.Id);

            result.ViewComponentName.Should().Be(nameof(ViewComponents.CustomerDetail));
        }

        [Test]
        public async Task CustomerDetail_should_return_correct_view()
        {

            var viewmodel = new CustomerViewModel { Customer = new CustomerDto { CustomerId = 1 } };

            _mediator.Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), default))
                .Returns(Task.FromResult(viewmodel));

            var query = new GetCustomerQuery { Id = 1 };


            var viewComponent = new CustomerDetail(_mediator.Object);

            var result = await viewComponent.InvokeAsync(query) as ViewViewComponentResult;

            var model = result.ViewData.Model as CustomerViewModel;

            model.Should().NotBeNull().And.BeOfType<CustomerViewModel>();

            model.Customer.Should().NotBeNull();

            model.Customer.CustomerId.Should().Be(viewmodel.Customer.CustomerId);

            _mediator.Verify(x => x.Send(It.Is<GetCustomerQuery>(q => q.Id == query.Id), default), Times.Once);
        }
    }
}