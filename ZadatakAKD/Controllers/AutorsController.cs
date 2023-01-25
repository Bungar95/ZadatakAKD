using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ZadatakAKD.Data;
using ZadatakAKD.Models;

namespace ZadatakAKD.Controllers
{
    public class AutorsController : Controller
    {
        private readonly ZadatakAKDContext _context;

        public AutorsController(ZadatakAKDContext context)
        {
            _context = context;
        }

        // GET: Autors
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["ImeSortParm"] = sortOrder == "Ime" ? "ime_desc" : "Ime";
            ViewData["PrezimeSortParm"] = sortOrder == "Prezime" ? "prezime_desc" : "Prezime";

            var zadatakAKDContext = from a in _context.Autors select a;

            switch (sortOrder)
            {
                case "Ime":
                    zadatakAKDContext = zadatakAKDContext.OrderBy(a => a.Ime);
                    break;
                case "ime_desc":
                    zadatakAKDContext = zadatakAKDContext.OrderByDescending(a => a.Ime);
                    break;
                case "Prezime":
                    zadatakAKDContext = zadatakAKDContext.OrderBy(a => a.Prezime);
                    break;
                case "prezime_desc":
                    zadatakAKDContext = zadatakAKDContext.OrderByDescending(a => a.Prezime);
                    break;
                default:
                    zadatakAKDContext = zadatakAKDContext.OrderBy(a => a.Id);
                    break;
            }

            return View(await zadatakAKDContext.AsNoTracking().ToListAsync());
        }

        // GET: Autors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .Include(a => a.Knjigas)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ime,Prezime")] Autor autor)
        {
            try
            {
                 _context.Add(autor);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
                
            } catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
            return View(autor);
        }

        // GET: Autors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return View(autor);
                    }
                }
        }

        // GET: Autors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Autors == null)
            {
                return NotFound();
            }

            var autor = await _context.Autors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Autors == null)
            {
                return Problem("Entity set 'ZadatakAKDContext.Autors'  is null.");
            }
            var autor = await _context.Autors.FindAsync(id);
            if (autor != null)
            {
                _context.Autors.Remove(autor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
          return _context.Autors.Any(e => e.Id == id);
        }
    }
}
