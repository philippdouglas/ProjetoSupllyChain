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
            var entradasPorMesPorProduto = _context.Entradas
                .GroupBy(e => new { e.DataHora.Year, e.DataHora.Month, e.Mercadoria.Nome })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Nome = g.Key.Nome,
                    Quantidade = g.Sum(e => e.Quantidade)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            var saidasPorMesPorProduto = _context.Saidas
                .GroupBy(s => new { s.DataHora.Year, s.DataHora.Month, s.Mercadoria.Nome })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Nome = g.Key.Nome,
                    Quantidade = -g.Sum(s => s.Quantidade) // Invert the quantity for the output (negative value)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            var entradasESaidasPorMesPorProduto = entradasPorMesPorProduto
                .Concat(saidasPorMesPorProduto)
                .GroupBy(es => new { es.Year, es.Month, es.Nome })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Nome = g.Key.Nome,
                    Entradas = g.Sum(es => es.Quantidade >= 0 ? es.Quantidade : 0),
                    Saidas = -g.Sum(es => es.Quantidade < 0 ? es.Quantidade : 0) // Invert the quantity for the output (negative value)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            var uniqueProductNames = entradasESaidasPorMesPorProduto.Select(g => g.Nome).Distinct().ToList();

            var labels = entradasESaidasPorMesPorProduto.Select(g => new DateTime(g.Year, g.Month, 1).ToString("MMM yyyy")).ToList();
            var datasets = new List<ChartDataset>();

            foreach (var productName in uniqueProductNames)
            {
                var entradasData = entradasESaidasPorMesPorProduto
                    .Where(g => g.Nome == productName)
                    .Select(g => g.Entradas)
                    .ToList();

                var saidasData = entradasESaidasPorMesPorProduto
                    .Where(g => g.Nome == productName)
                    .Select(g => g.Saidas)
                    .ToList();

                datasets.Add(new ChartDataset
                {
                    Label = $"{productName} - Entradas",
                    BackgroundColor = "Orange",
                    Data = entradasData
                });

                datasets.Add(new ChartDataset
                {
                    Label = $"{productName} - Saídas",
                    BackgroundColor = "green",
                    Data = saidasData
                });
            }

            var chartData = new ChartData
            {
                Labels = labels,
                Datasets = datasets
            };

            return Json(chartData);
        }
    }
}
