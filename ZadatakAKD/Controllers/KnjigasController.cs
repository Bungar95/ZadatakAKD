using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZadatakAKD.Data;
using ZadatakAKD.Models;

namespace ZadatakAKD.Controllers
{
    public class KnjigasController : Controller
    {
        private readonly ZadatakAKDContext _context;

        public KnjigasController(ZadatakAKDContext context)
        {
            _context = context;
        }

        // GET: Knjigas
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
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "Id");
            return View();
        }

        // POST: Knjigas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AutorId,Naslov,GodinaIzdavanja")] Knjiga knjiga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knjiga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "Id", knjiga.AutorId);
            return View(knjiga);
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
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "Id", knjiga.AutorId);
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knjiga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnjigaExists(knjiga.Id))
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
            ViewData["AutorId"] = new SelectList(_context.Autors, "Id", "Id", knjiga.AutorId);
            return View(knjiga);
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
    }
}
