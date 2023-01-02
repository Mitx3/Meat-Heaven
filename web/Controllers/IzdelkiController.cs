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
    public class IzdelkiController : Controller
    {
        private readonly TrgovinaContext _context;

        public IzdelkiController(TrgovinaContext context)
        {
            _context = context;
        }

        // GET: Izdelki
        /*public async Task<IActionResult> Index()
        {
            var trgovinaContext = _context.Izdelki.Include(i => i.Oddelek);
            return View(await trgovinaContext.ToListAsync());
        }*/


        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Ime_desc" : "";
            ViewData["CenaSortParm"] = sortOrder == "Cena" ? "cena_desc" : "Cena";
            ViewData["OdIDSortParm"] = sortOrder == "ID" ? "ID_desc" : "ID";
            
            ViewData["CurrentSort"] = sortOrder;


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewData["CurrentFilter"] = searchString;


            var izdelki = from s in _context.Izdelki.Include(i => i.Oddelek)
                        select s;

              if (!String.IsNullOrEmpty(searchString))
                {
                    izdelki = izdelki.Where(s => s.IzdelekIme.Contains(searchString)
                                        || s.IzdelekVrsta.Contains(searchString));
                }

            switch (sortOrder)
            {
                case "Ime_desc":
                    izdelki = izdelki.OrderByDescending(s => s.IzdelekIme);
                    break;
                case "Cena":
                    izdelki = izdelki.OrderBy(s => s.IzdelekCena);
                    break;
                case "cena_desc":
                    izdelki = izdelki.OrderByDescending(s => s.IzdelekCena);
                    break;
                case "ID":
                    izdelki = izdelki.OrderBy(s => s.OddelekID);
                    break;    
                case "ID_desc":
                    izdelki = izdelki.OrderByDescending(s => s.OddelekID);
                    break; 
                default:
                    izdelki = izdelki.OrderBy(s => s.IzdelekIme);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Izdelek>.CreateAsync(izdelki.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View(await students.AsNoTracking().ToListAsync());
}



        // GET: Izdelki/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Izdelki == null)
            {
                return NotFound();
            }

            var izdelek = await _context.Izdelki
                .Include(i => i.Oddelek)
                .FirstOrDefaultAsync(m => m.IzdelekID == id);
            if (izdelek == null)
            {
                return NotFound();
            }

            return View(izdelek);
        }

        // GET: Izdelki/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["OddelekID"] = new SelectList(_context.Oddelki, "OddelekID", "OddelekID");
            return View();
        }

        // POST: Izdelki/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("IzdelekIme,IzdelekVrsta,IzdelekCena,RokProizvodnje,RokUporabe,OddelekID")] Izdelek izdelek)
        {
            try
            {
            if (ModelState.IsValid)
            {
                _context.Add(izdelek);
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

            ViewData["OddelekID"] = new SelectList(_context.Oddelki, "OddelekID", "OddelekID", izdelek.OddelekID);
            return View(izdelek);
        }

        // GET: Izdelki/Edit/5
        [Authorize]
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
            ViewData["OddelekID"] = new SelectList(_context.Oddelki, "OddelekID", "OddelekID", izdelek.OddelekID);
            return View(izdelek);
        }

        // POST: Izdelki/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("IzdelekID,IzdelekIme,IzdelekVrsta,IzdelekCena,RokProizvodnje,RokUporabe,OddelekID")] Izdelek izdelek)
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
            ViewData["OddelekID"] = new SelectList(_context.Oddelki, "OddelekID", "OddelekID", izdelek.OddelekID);
            return View(izdelek);
        }

        // GET: Izdelki/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Izdelki == null)
            {
                return NotFound();
            }

            var izdelek = await _context.Izdelki
                .Include(i => i.Oddelek)
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
        [Authorize]
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
