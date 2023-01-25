using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZadatakAKD.Data;
using ZadatakAKD.Models;

namespace ZadatakAKD.Controllers
{
    [Authorize]
    public class KnjigasController : Controller
    {
        private readonly ZadatakAKDContext _context;

        public KnjigasController(ZadatakAKDContext context)
        {
            _context = context;
        }

        // GET: Knjigas
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var zadatakAKDContext = _context.Knjigas.Include(k => k.Autor);
            return View(await zadatakAKDContext.ToListAsync());
        }

        // GET: Knjigas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Knjigas == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjigas
                .Include(k => k.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }

        // GET: Knjigas/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "PunoIme");
            return View();
        }

        // POST: Knjigas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutorId,Naslov,GodinaIzdavanja")] Knjiga knjiga)
        {
            try
            {
                _context.Add(knjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch (Exception ex)
            {
                ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "PunoIme", knjiga.AutorId);
                return View(knjiga);
            }          
        }

        // GET: Knjigas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Knjigas == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjigas.FindAsync(id);
            if (knjiga == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "PunoIme", knjiga.AutorId);
            return View(knjiga);
        }

        // POST: Knjigas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AutorId,Naslov,GodinaIzdavanja")] Knjiga knjiga)
        {
            if (id != knjiga.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(knjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KnjigaExists(knjiga.Id))
                {
                    return NotFound();
                }
                else
                {
                    ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "PunoIme", knjiga.AutorId);
                    return View(knjiga);
                }
            }  
        }

        // GET: Knjigas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Knjigas == null)
            {
                return NotFound();
            }

            var knjiga = await _context.Knjigas
                .Include(k => k.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knjiga == null)
            {
                return NotFound();
            }

            return View(knjiga);
        }

        // POST: Knjigas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Knjigas == null)
            {
                return Problem("Entity set 'ZadatakAKDContext.Knjigas'  is null.");
            }
            var knjiga = await _context.Knjigas.FindAsync(id);
            if (knjiga != null)
            {
                _context.Knjigas.Remove(knjiga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnjigaExists(int id)
        {
          return _context.Knjigas.Any(e => e.Id == id);
        }

        // EXPORT: Knjigas/Export
        [HttpGet, ActionName("Export")]
        public IActionResult Export()
        {
            var toDownload = _context.Knjigas;
            HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=export_knjige.json");
            return new JsonResult(toDownload);
        }
    }
}
