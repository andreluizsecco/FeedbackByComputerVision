using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StatisticsWebApp.Data;

namespace StatisticsWebApp.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly Repository _repository;

        public StatisticsController(IConfiguration configuration) =>
            _repository = new Repository(configuration);

        public async Task<IActionResult> Index(string source)
        {
            var chartData = new Dictionary<string, List<PieSeriesData>>();

            chartData.Add("Gender", await CreatePieChart(source, "Gender"));
            chartData.Add("Age", await CreatePieChart(source, "Age"));
            chartData.Add("Emotion", await CreatePieChart(source, "Emotion"));
            chartData.Add("Glasses", await CreatePieChart(source, "Glasses"));
            chartData.Add("HairColor", await CreatePieChart(source, "HairColor"));

            ViewBag.ChartData = chartData;
            return View();
        }

        private async Task<List<PieSeriesData>> CreatePieChart(string source, string info)
        {
            var pieData = new List<PieSeriesData>();
            var dataList = await _repository.GetStatistics(source, info);

            foreach (var data in dataList)
                pieData.Add(new PieSeriesData { Name = data.Key, Y = (double)data.Value });

            return pieData;
        }
    }
}