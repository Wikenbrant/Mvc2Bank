using System.Diagnostics;
using System.Threading.Tasks;
using Application.Customers.Queries.GetCustomersPagination;
using Application.Customers.Queries.GetCustomersPaginationOrderOn;
using Application.Customers.Queries.GetTop10CustomersByCountry;
using Application.Search.Query;
using Application.Statistics.Query.GetNumberOfAccountsAndTotalSumForEachCountry;
using Domain.SearchModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Countries = await _mediator.Send(new GetNumberOfAccountsAndTotalSumForEachCountryQuery()).ConfigureAwait(false)
            };

            return View(model);
        }

        [ResponseCache(CacheProfileName = "Country60")]
        public IActionResult GetTop10InCountry([FromQuery]GetTop10CustomersByCountryQuery query)
        {
            return ViewComponent(nameof(ViewComponents.GetTop10InCountry),new { query });
        }

        public IActionResult StatisticsCountries()
        {
            return ViewComponent(nameof(ViewComponents.StatisticsCountries));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
