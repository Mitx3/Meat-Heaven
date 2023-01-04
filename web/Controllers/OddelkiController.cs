using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    //[Authorize]
    public class OddelkiController : Controller
    {
        private readonly TrgovinaContext _context;

        public OddelkiController(TrgovinaContext context)
        {
            _context = context;
        }

        // GET: Oddelki
        public async Task<IActionResult> Index()
        {
              return _context.Oddelki != null ? 
                          View(await _context.Oddelki.ToListAsync()) :
                          Problem("Entity set 'TrgovinaContext.Oddelki'  is null.");
        }

        // GET: Oddelki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Oddelki == null)
            {
                return NotFound();
            }

            //var oddelek = await _context.Oddelki
            //    .FirstOrDefaultAsync(m => m.OddelekID == id);
            var oddelek = await _context.Oddelki
                .Include(s => s.Izdelki)
                    
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.OddelekID == id);

            if (oddelek == null)
            {
                return NotFound();
            }

            return View(oddelek);
        }

        // GET: Oddelki/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Oddelki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OddelekID,OddelekIme,VrstaIzdelkov,KmetijaID")] Oddelek oddelek)
        {
            try
            {
            if (ModelState.IsValid)
            {
                _context.Add(oddelek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            }

            return View(oddelek);
        }

        // GET: Oddelki/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Oddelki == null)
            {
                return NotFound();
            }

            var oddelek = await _context.Oddelki.FindAsync(id);
            if (oddelek == null)
            {
                return NotFound();
            }
            return View(oddelek);
        }

        // POST: Oddelki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OddelekID,OddelekIme,VrstaIzdelkov,KmetijaID")] Oddelek oddelek)
        {
            if (id != oddelek.OddelekID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oddelek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OddelekExists(oddelek.OddelekID))
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
            return View(oddelek);
        }

        // GET: Oddelki/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Oddelki == null)
            {
                return NotFound();
            }

            var oddelek = await _context.Oddelki
                .FirstOrDefaultAsync(m => m.OddelekID == id);
            if (oddelek == null)
            {
                return NotFound();
            }

            return View(oddelek);
        }

        // POST: Oddelki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Oddelki == null)
            {
                return Problem("Entity set 'TrgovinaContext.Oddelki'  is null.");
            }
            var oddelek = await _context.Oddelki.FindAsync(id);
            if (oddelek != null)
            {
                _context.Oddelki.Remove(oddelek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OddelekExists(int id)
        {
          return (_context.Oddelki?.Any(e => e.OddelekID == id)).GetValueOrDefault();
        }
    }
}
