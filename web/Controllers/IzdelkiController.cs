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
    public class IzdelkiController : Controller
    {
        private readonly TrgovinaContext _context;

        public IzdelkiController(TrgovinaContext context)
        {
            _context = context;
        }

        // GET: Izdelki
        public async Task<IActionResult> Index()
        {
              return _context.Izdelki != null ? 
                          View(await _context.Izdelki.ToListAsync()) :
                          Problem("Entity set 'TrgovinaContext.Izdelki'  is null.");
        }

        // GET: Izdelki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Izdelki == null)
            {
                return NotFound();
            }

            var izdelek = await _context.Izdelki
                .FirstOrDefaultAsync(m => m.IzdelekID == id);
            if (izdelek == null)
            {
                return NotFound();
            }

            return View(izdelek);
        }

        // GET: Izdelki/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Izdelki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IzdelekID,IzdelekIme,IzdelekVrsta,IzdelekCena,RokNakupa")] Izdelek izdelek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(izdelek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(izdelek);
        }

        // GET: Izdelki/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Izdelki == null)
            {
                return NotFound();
            }

            var izdelek = await _context.Izdelki.FindAsync(id);
            if (izdelek == null)
            {
                return NotFound();
            }
            return View(izdelek);
        }

        // POST: Izdelki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IzdelekID,IzdelekIme,IzdelekVrsta,IzdelekCena,RokNakupa")] Izdelek izdelek)
        {
            if (id != izdelek.IzdelekID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(izdelek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IzdelekExists(izdelek.IzdelekID))
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
            return View(izdelek);
        }

        // GET: Izdelki/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Izdelki == null)
            {
                return NotFound();
            }

            var izdelek = await _context.Izdelki
                .FirstOrDefaultAsync(m => m.IzdelekID == id);
            if (izdelek == null)
            {
                return NotFound();
            }

            return View(izdelek);
        }

        // POST: Izdelki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Izdelki == null)
            {
                return Problem("Entity set 'TrgovinaContext.Izdelki'  is null.");
            }
            var izdelek = await _context.Izdelki.FindAsync(id);
            if (izdelek != null)
            {
                _context.Izdelki.Remove(izdelek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IzdelekExists(int id)
        {
          return (_context.Izdelki?.Any(e => e.IzdelekID == id)).GetValueOrDefault();
        }
    }
}
