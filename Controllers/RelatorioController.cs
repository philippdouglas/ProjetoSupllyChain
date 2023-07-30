using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SupplyChain.Models;

namespace SupplyChain.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly LogisticaDbContext _context;

        public RelatorioController(LogisticaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetChartData()
        {
            var entradasPorMes = _context.Entradas
                .GroupBy(e => new { e.DataHora.Year, e.DataHora.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Quantidade = g.Sum(e => e.Quantidade)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            var saidasPorMes = _context.Saidas
                .GroupBy(s => new { s.DataHora.Year, s.DataHora.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Quantidade = -g.Sum(s => s.Quantidade) // Invert the quantity for the output (negative value)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            var entradasESaidasPorMes = entradasPorMes
                .Concat(saidasPorMes)
                .GroupBy(es => new { es.Year, es.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Entradas = g.Sum(es => es.Quantidade >= 0 ? es.Quantidade : 0),
                    Saidas = g.Sum(es => es.Quantidade < 0 ? es.Quantidade : 0)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            var labels = entradasESaidasPorMes.Select(g => new DateTime(g.Year, g.Month, 1).ToString("MMM yyyy")).ToList();
            var entradasData = entradasESaidasPorMes.Select(g => g.Entradas).ToList();
            var saidasData = entradasESaidasPorMes.Select(g => g.Saidas).ToList();

            var datasets = new List<ChartDataset>
            {
                new ChartDataset { Label = "Entradas", BackgroundColor = "red", Data = entradasData },
                new ChartDataset { Label = "Saídas", BackgroundColor = "green", Data = saidasData }
            };

            var chartData = new ChartData
            {
                Labels = labels,
                Datasets = datasets
            };

            return Json(chartData);
        }
    }
}

