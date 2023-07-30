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
    public class EntradaController : Controller
    {
        private readonly LogisticaDbContext _context;

        public EntradaController(LogisticaDbContext context)
        {
            _context = context;
        }

        public IActionResult CadastroEntrada()
        {
            var mercadorias = _context.Mercadorias.ToList();
            var entradasCadastradas = _context.Entradas.Include(e => e.Mercadoria).OrderByDescending(e => e.DataHora).ToList();

            // Atualizar o estoque para cada mercadoria com base nas entradas cadastradas
            foreach (var mercadoria in mercadorias)
            {
                int estoque = entradasCadastradas
                    .Where(e => e.MercadoriaId == mercadoria.Id)
                    .Sum(e => e.Quantidade);

                mercadoria.Estoque = estoque;
            }

            ViewBag.EntradasCadastradas = entradasCadastradas;
            return View(mercadorias);
        }

        [HttpPost]
        public IActionResult SalvarEntrada(Entrada entrada)
        {
            if (ModelState.IsValid)
            {
                // Encontrar a mercadoria selecionada para obter os dados adicionais
                var mercadoriaSelecionada = _context.Mercadorias.Find(entrada.MercadoriaId);

                // Criar uma nova entrada com os valores da mercadoria e os dados do formulário
                var novaEntrada = new Entrada
                {
                    MercadoriaId = entrada.MercadoriaId,
                    DataHora = entrada.DataHora,
                    Local = entrada.Local,
                    Quantidade = entrada.Quantidade,
                    Nome = mercadoriaSelecionada.Nome, // Preencher o Nome da mercadoria
                    NumeroRegistro = mercadoriaSelecionada.NumeroRegistro, // Preencher o Número de Registro da mercadoria
                    Fabricante = mercadoriaSelecionada.Fabricante, // Preencher o Fabricante da mercadoria
                    Tipo = mercadoriaSelecionada.Tipo, // Preencher o Tipo da mercadoria
                    Descricao = mercadoriaSelecionada.Descricao, // Preencher a Descrição da mercadoria
                    Mercadoria = mercadoriaSelecionada // Relaciona a mercadoria à entrada
                };

                _context.Entradas.Add(novaEntrada);
                _context.SaveChanges();

                // Atualizar o estoque para a mercadoria relacionada à entrada cadastrada
                var mercadoria = _context.Mercadorias
                    .Include(m => m.Entradas)
                    .FirstOrDefault(m => m.Id == entrada.MercadoriaId);

                if (mercadoria != null)
                {
                    int estoque = mercadoria.Entradas.Sum(e => e.Quantidade);
                    mercadoria.Estoque = estoque;
                    _context.SaveChanges();
                }
            }

            var mercadorias = _context.Mercadorias.ToList();
            var entradasCadastradas = _context.Entradas.Include(e => e.Mercadoria).ToList();

            // Atualizar o estoque para cada mercadoria com base nas entradas cadastradas
            foreach (var mercadoria in mercadorias)
            {
                int estoque = entradasCadastradas
                    .Where(e => e.MercadoriaId == mercadoria.Id)
                    .Sum(e => e.Quantidade);

                mercadoria.Estoque = estoque;
            }

            ViewBag.EntradasCadastradas = entradasCadastradas;
            return View("CadastroEntrada", mercadorias);
        }

        public IActionResult ConfirmarExclusao(int id)
        {
            var entrada = _context.Entradas
                .Include(e => e.Mercadoria)
                .SingleOrDefault(e => e.Id == id);

            if (entrada == null)
            {
                return RedirectToAction("CadastroEntrada");
            }

            // Modifique aqui para retornar uma coleção com um único item
            return View(new List<Entrada> { entrada });
        }

        [HttpPost]
        public IActionResult ExcluirEntrada(int id)
        {
            var entrada = _context.Entradas.Find(id);

            if (entrada != null)
            {
                _context.Entradas.Remove(entrada);
                _context.SaveChanges();
            }

            return RedirectToAction("CadastroEntrada");
        }

        public IActionResult ExportarEntradasParaExcel()
        {
            var entradas = _context.Entradas
                .Include(e => e.Mercadoria)
                .ToList();

            var nomeArquivo = $"Entradas_{DateTime.Now:yyyy_MM_dd_HH_mm}.xlsx";

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Entradas");

                // Defina as colunas do arquivo Excel
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Nome";
                worksheet.Cells["C1"].Value = "Número de Registro";
                worksheet.Cells["D1"].Value = "Fabricante";
                worksheet.Cells["E1"].Value = "Tipo";
                worksheet.Cells["F1"].Value = "Descrição";
                worksheet.Cells["G1"].Value = "Quantidade";
                worksheet.Cells["H1"].Value = "Data de Entrada";
                worksheet.Cells["I1"].Value = "Local";
                worksheet.Cells["J1"].Value = "Registro";

                // Preencha os dados nas células do Excel
                for (int i = 0; i < entradas.Count; i++)
                {
                    var entrada = entradas[i];
                    worksheet.Cells[i + 2, 1].Value = entrada.Id;
                    worksheet.Cells[i + 2, 2].Value = entrada.Mercadoria.Nome;
                    worksheet.Cells[i + 2, 3].Value = entrada.Mercadoria.NumeroRegistro;
                    worksheet.Cells[i + 2, 4].Value = entrada.Mercadoria.Fabricante;
                    worksheet.Cells[i + 2, 5].Value = entrada.Mercadoria.Tipo;
                    worksheet.Cells[i + 2, 6].Value = entrada.Mercadoria.Descricao;
                    worksheet.Cells[i + 2, 7].Value = entrada.Quantidade;
                    worksheet.Cells[i + 2, 8].Value = entrada.DataHora.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 9].Value = entrada.Local;
                    worksheet.Cells[i + 2, 10].Value = "Entrada";
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

        public IActionResult SelecionarDataExportacao()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExportarEntradasParaExcel(int mes, int ano)
        {
            var entradas = _context.Entradas
                .Include(e => e.Mercadoria)
                .Where(e => e.DataHora.Month == mes && e.DataHora.Year == ano)
                .ToList();

            var nomeArquivo = $"Entradas_{ano:D4}_{mes:D2}.xlsx";

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Entradas");

                // Defina as colunas do arquivo Excel
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Nome";
                worksheet.Cells["C1"].Value = "Número de Registro";
                worksheet.Cells["D1"].Value = "Fabricante";
                worksheet.Cells["E1"].Value = "Tipo";
                worksheet.Cells["F1"].Value = "Descrição";
                worksheet.Cells["G1"].Value = "Quantidade";
                worksheet.Cells["H1"].Value = "Data de Entrada";
                worksheet.Cells["I1"].Value = "Local";
                worksheet.Cells["J1"].Value = "Registro";

                // Preencha os dados nas células do Excel
                for (int i = 0; i < entradas.Count; i++)
                {
                    var entrada = entradas[i];
                    worksheet.Cells[i + 2, 1].Value = entrada.Id;
                    worksheet.Cells[i + 2, 2].Value = entrada.Mercadoria.Nome;
                    worksheet.Cells[i + 2, 3].Value = entrada.Mercadoria.NumeroRegistro;
                    worksheet.Cells[i + 2, 4].Value = entrada.Mercadoria.Fabricante;
                    worksheet.Cells[i + 2, 5].Value = entrada.Mercadoria.Tipo;
                    worksheet.Cells[i + 2, 6].Value = entrada.Mercadoria.Descricao;
                    worksheet.Cells[i + 2, 7].Value = entrada.Quantidade;
                    worksheet.Cells[i + 2, 8].Value = entrada.DataHora.ToString("dd/MM/yyyy");
                    worksheet.Cells[i + 2, 9].Value = entrada.Local;
                    worksheet.Cells[i + 2, 10].Value = "Entrada";
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
