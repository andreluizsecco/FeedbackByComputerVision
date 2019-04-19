using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StatisticsWebApp.Data;
using StatisticsWebApp.Models;

namespace StatisticsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository _repository;

        public HomeController(IConfiguration configuration) =>
            _repository = new Repository(configuration);

        public async Task<IActionResult> Index()
        {
            ViewBag.Sources = await _repository.GetSources();
            return View();
        }

        public async Task<IActionResult> ClearFaceStatistics()
        {
            await _repository.ClearFaceStatistics();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearAdvertisingStatistics()
        {
            await _repository.ClearAdvertisingStatistics();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
