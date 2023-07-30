using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using Rotativa.AspNetCore;

namespace SupplyChain.Controllers
{
    public class SaidaController : Controller
    {
        private readonly LogisticaDbContext _context;

        public SaidaController(LogisticaDbContext context)
        {
            _context = context;
        }

        public IActionResult CadastroSaida()
        {
            var mercadorias = _context.Mercadorias.ToList();
            var saidasCadastradas = _context.Saidas.Include(s => s.Mercadoria).OrderByDescending(s => s.DataHora).ToList();

            // Atualizar o estoque para cada mercadoria com base nas saídas cadastradas
            foreach (var mercadoria in mercadorias)
            {
                int estoque = saidasCadastradas
                    .Where(s => s.MercadoriaId == mercadoria.Id)
                    .Sum(s => s.Quantidade);

                mercadoria.Estoque = estoque;
            }

            ViewBag.SaidasCadastradas = saidasCadastradas;
            return View(mercadorias);
        }

        [HttpPost]
        public IActionResult SalvarSaida(Saida saida)
        {
            if (ModelState.IsValid)
            {
                _context.Saidas.Add(saida);
                _context.SaveChanges();

                // Atualizar o estoque para a mercadoria relacionada à saída cadastrada
                var mercadoria = _context.Mercadorias
                    .Include(m => m.Saidas)
                    .FirstOrDefault(m => m.Id == saida.MercadoriaId);

                if (mercadoria != null)
                {
                    int estoque = mercadoria.Saidas.Sum(s => s.Quantidade);
                    mercadoria.Estoque = estoque;
                    _context.SaveChanges();
                }
            }

            var mercadorias = _context.Mercadorias.ToList();
            var saidasCadastradas = _context.Saidas.Include(s => s.Mercadoria).OrderByDescending(s => s.DataHora).ToList();

            // Atualizar o estoque para cada mercadoria com base nas saídas cadastradas
            foreach (var mercadoria in mercadorias)
            {
                int estoque = saidasCadastradas
                    .Where(s => s.MercadoriaId == mercadoria.Id)
                    .Sum(s => s.Quantidade);

                mercadoria.Estoque = estoque;
            }

            ViewBag.SaidasCadastradas = saidasCadastradas;
            return View("CadastroSaida", mercadorias);
        }

        public IActionResult ConfirmarExclusao(int id)
        {
            var saida = _context.Saidas
                .Include(s => s.Mercadoria)
                .SingleOrDefault(s => s.Id == id);

            if (saida == null)
            {
                return RedirectToAction("CadastroSaida");
            }

            // Modifique aqui para retornar uma coleção com um único item
            return View(new List<Saida> { saida });
        }

        [HttpPost]
        public IActionResult ExcluirSaida(int id)
        {
            var saida = _context.Saidas.Find(id);

            if (saida != null)
            {
                _context.Saidas.Remove(saida);
                _context.SaveChanges();
            }

            return RedirectToAction("CadastroSaida");
        }

        public IActionResult ExportarSaidasParaExcel()
        {
            var saidas = _context.Saidas
                .Include(s => s.Mercadoria)
                .ToList();

            var nomeArquivo = $"Saidas_{DateTime.Now:yyyy_MM_dd_HH_mm}.xlsx";

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Saidas");

                // Defina as colunas do arquivo Excel
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Nome";
                worksheet.Cells["C1"].Value = "Número de Registro";
                worksheet.Cells["D1"].Value = "Fabricante";
                worksheet.Cells["E1"].Value = "Tipo";
                worksheet.Cells["F1"].Value = "Descrição";
                worksheet.Cells["G1"].Value = "Quantidade";
                worksheet.Cells["H1"].Value = "Data de Saída";
                worksheet.Cells["I1"].Value = "Local";
                worksheet.Cells["J1"].Value = "Registro";

                // Preencha os dados nas células do Excel
                for (int i = 0; i < saidas.Count; i++)
                {
                    var saida = saidas[i];
                    worksheet.Cells[i + 2, 1].Value = saida.Id;
                    worksheet.Cells[i + 2, 2].Value = saida.Mercadoria.Nome;
                    worksheet.Cells[i + 2, 3].Value = saida.Mercadoria.NumeroRegistro;
                    worksheet.Cells[i + 2, 4].Value = saida.Mercadoria.Fabricante;
                    worksheet.Cells[i + 2, 5].Value = saida.Mercadoria.Tipo;
                    worksheet.Cells[i + 2, 6].Value = saida.Mercadoria.Descricao;
                    worksheet.Cells[i + 2, 7].Value = saida.Quantidade;
                    worksheet.Cells[i + 2, 8].Value = saida.DataHora.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 9].Value = saida.Local;
                    worksheet.Cells[i + 2, 10].Value = "Saída";
                }

                // Ajuste o estilo da primeira linha (cabeçalho)
                worksheet.Cells["A1:J1"].Style.Font.Bold = true;

                // Ajuste o tamanho das colunas automaticamente
                worksheet.Cells.AutoFitColumns();

                var fileStream = new MemoryStream(package.GetAsByteArray());

                // Retorne o arquivo Excel para download com o nome definido
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
            }
        }

        public IActionResult SelecionarDataExportacaoSaida()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExportarSaidaParaExcel(int mes, int ano)
        {
            var saidas = _context.Saidas
                .Include(s => s.Mercadoria)
                .Where(s => s.DataHora.Month == mes && s.DataHora.Year == ano)
                .ToList();

            var nomeArquivo = $"Saidas_{ano:D4}_{mes:D2}.xlsx";

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Saidas");

                // Defina as colunas do arquivo Excel
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Nome";
                worksheet.Cells["C1"].Value = "Número de Registro";
                worksheet.Cells["D1"].Value = "Fabricante";
                worksheet.Cells["E1"].Value = "Tipo";
                worksheet.Cells["F1"].Value = "Descrição";
                worksheet.Cells["G1"].Value = "Quantidade";
                worksheet.Cells["H1"].Value = "Data de Saída";
                worksheet.Cells["I1"].Value = "Local";
                worksheet.Cells["J1"].Value = "Registro";

                // Preencha os dados nas células do Excel
                for (int i = 0; i < saidas.Count; i++)
                {
                    var saida = saidas[i];
                    worksheet.Cells[i + 2, 1].Value = saida.Id;
                    worksheet.Cells[i + 2, 2].Value = saida.Mercadoria.Nome;
                    worksheet.Cells[i + 2, 3].Value = saida.Mercadoria.NumeroRegistro;
                    worksheet.Cells[i + 2, 4].Value = saida.Mercadoria.Fabricante;
                    worksheet.Cells[i + 2, 5].Value = saida.Mercadoria.Tipo;
                    worksheet.Cells[i + 2, 6].Value = saida.Mercadoria.Descricao;
                    worksheet.Cells[i + 2, 7].Value = saida.Quantidade;
                    worksheet.Cells[i + 2, 8].Value = saida.DataHora.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 9].Value = saida.Local;
                    worksheet.Cells[i + 2, 10].Value = "Saída";
                }

                // Ajuste o estilo da primeira linha (cabeçalho)
                worksheet.Cells["A1:J1"].Style.Font.Bold = true;

                // Ajuste o tamanho das colunas automaticamente
                worksheet.Cells.AutoFitColumns();

                var fileStream = new MemoryStream(package.GetAsByteArray());

                // Retorne o arquivo Excel para download com o nome definido
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
            }
        }
    }
}
