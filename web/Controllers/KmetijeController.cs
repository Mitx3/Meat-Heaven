using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class KmetijeController : Controller
    {
        private readonly TrgovinaContext _context;

        public KmetijeController(TrgovinaContext context)
        {
            _context = context;
        }

        // GET: Kmetije
        public async Task<IActionResult> Index()
        {
              return _context.Kmetije != null ? 
                          View(await _context.Kmetije.ToListAsync()) :
                          Problem("Entity set 'TrgovinaContext.Kmetije'  is null.");
        }

        // GET: Kmetije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kmetije == null)
            {
                return NotFound();
            }

            var kmetija = await _context.Kmetije
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kmetija == null)
            {
                return NotFound();
            }

            return View(kmetija);
        }

        // GET: Kmetije/Create
        
        public IActionResult Create()
        {
            return View();
        }
        

        // POST: Kmetije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Lastnik,Lokacija")] Kmetija kmetija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kmetija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kmetija);
        }

        // GET: Kmetije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kmetije == null)
            {
                return NotFound();
            }

            var kmetija = await _context.Kmetije.FindAsync(id);
            if (kmetija == null)
            {
                return NotFound();
            }
            return View(kmetija);
        }

        // POST: Kmetije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Lastnik,Lokacija")] Kmetija kmetija)
        {
            if (id != kmetija.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kmetija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KmetijaExists(kmetija.ID))
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
            return View(kmetija);
        }

        // GET: Kmetije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kmetije == null)
            {
                return NotFound();
            }

            var kmetija = await _context.Kmetije
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kmetija == null)
            {
                return NotFound();
            }

            return View(kmetija);
        }

        // POST: Kmetije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kmetije == null)
            {
                return Problem("Entity set 'TrgovinaContext.Kmetije'  is null.");
            }
            var kmetija = await _context.Kmetije.FindAsync(id);
            if (kmetija != null)
            {
                _context.Kmetije.Remove(kmetija);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KmetijaExists(int id)
        {
          return (_context.Kmetije?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
