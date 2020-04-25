using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Customers.Queries.GetTop10CustomersByCountry;
using Application.Statistics.Query.GetNumberOfAccountsAndTotalSumForEachCountry;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
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
            }; return View(model);
        }

        public async Task<IActionResult> GetTop10InCountry([FromQuery]GetTop10CustomersByCountryQuery query)
        {
            var model = await _mediator.Send(query).ConfigureAwait(false);
            return View("_GetTop10InCountry",model);
        }

        public async Task<IActionResult> StatisticsCountries()
        {
            var model = await _mediator.Send(new GetNumberOfAccountsAndTotalSumForEachCountryQuery()).ConfigureAwait(false);
            return View("_StatisticsCountries",model);
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
