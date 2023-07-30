using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace SupplyChain.Controllers
{
    public class MercadoriaController : Controller
    {
        private readonly LogisticaDbContext _db;

        public MercadoriaController(LogisticaDbContext db)
        {
            _db = db;
        }

        // Função para atualizar o estoque das mercadorias
        private async Task AtualizarEstoque()
        {
            var mercadorias = await _db.Mercadorias.ToListAsync();

            foreach (var mercadoria in mercadorias)
            {
                var entradas = await _db.Entradas
                    .Where(e => e.MercadoriaId == mercadoria.Id)
                    .ToListAsync();

                var saidas = await _db.Saidas
                    .Where(s => s.MercadoriaId == mercadoria.Id)
                    .ToListAsync();

                int quantidadeEntradas = entradas.Sum(e => e.Quantidade);
                int quantidadeSaidas = saidas.Sum(s => s.Quantidade);

                mercadoria.Estoque = quantidadeEntradas - quantidadeSaidas;

                _db.Update(mercadoria);
            }

            await _db.SaveChangesAsync();
        }

        // GET: /Mercadoria/
        public async Task<IActionResult> Index()
        {
            await AtualizarEstoque();

            var mercadorias = await _db.Mercadorias.OrderByDescending(m => m.Estoque).ToListAsync();
            return View(mercadorias);
        }

        // GET: /Mercadoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Mercadoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mercadoria mercadoria)
        {
            if (ModelState.IsValid)
            {
                _db.Add(mercadoria);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mercadoria);
        }

        // GET: /Mercadoria/Edit/1 (exemplo de rota)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mercadoria = await _db.Mercadorias.FindAsync(id);
            if (mercadoria == null)
            {
                return NotFound();
            }

            return View(mercadoria);
        }

        // POST: /Mercadoria/Edit/1 (exemplo de rota)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mercadoria mercadoria)
        {
            if (id != mercadoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(mercadoria);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MercadoriaExists(mercadoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mercadoria);
        }

        // GET: /Mercadoria/Details/1 (exemplo de rota)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mercadoria = await _db.Mercadorias.FirstOrDefaultAsync(m => m.Id == id);

            if (mercadoria == null)
            {
                return NotFound();
            }

            return View(mercadoria);
        }

        // GET: /Mercadoria/Delete/1 (exemplo de rota)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mercadoria = await _db.Mercadorias.FirstOrDefaultAsync(m => m.Id == id);

            if (mercadoria == null)
            {
                return NotFound();
            }

            return View(mercadoria);
        }

        // POST: /Mercadoria/Delete/1 (exemplo de rota)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mercadoria = await _db.Mercadorias.FindAsync(id);
            if (mercadoria == null)
            {
                return NotFound();
            }

            _db.Mercadorias.Remove(mercadoria);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MercadoriaExists(int id)
        {
            return _db.Mercadorias.Any(e => e.Id == id);
        }
    }
}
