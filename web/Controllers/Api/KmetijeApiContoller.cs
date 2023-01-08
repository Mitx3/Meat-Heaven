using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_Api
{
    [Route("api/v1/Kmetije")]
    [ApiController]
    public class KmetijeApiContoller : ControllerBase
    {
        private readonly TrgovinaContext _context;

        public KmetijeApiContoller(TrgovinaContext context)
        {
            _context = context;
        }

        // GET: api/KmetijeApiContoller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kmetija>>> GetKmetije()
        {
          if (_context.Kmetije == null)
          {
              return NotFound();
          }
            return await _context.Kmetije.ToListAsync();
        }

        // GET: api/KmetijeApiContoller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kmetija>> GetKmetija(int id)
        {
          if (_context.Kmetije == null)
          {
              return NotFound();
          }
            var kmetija = await _context.Kmetije.FindAsync(id);

            if (kmetija == null)
            {
                return NotFound();
            }

            return kmetija;
        }

        // PUT: api/KmetijeApiContoller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKmetija(int id, Kmetija kmetija)
        {
            if (id != kmetija.ID)
            {
                return BadRequest();
            }

            _context.Entry(kmetija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KmetijaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KmetijeApiContoller
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Kmetija>> PostKmetija(Kmetija kmetija)
        {
          if (_context.Kmetije == null)
          {
              return Problem("Entity set 'TrgovinaContext.Kmetije'  is null.");
          }
            _context.Kmetije.Add(kmetija);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKmetija", new { id = kmetija.ID }, kmetija);
        }

        // DELETE: api/KmetijeApiContoller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKmetija(int id)
        {
            if (_context.Kmetije == null)
            {
                return NotFound();
            }
            var kmetija = await _context.Kmetije.FindAsync(id);
            if (kmetija == null)
            {
                return NotFound();
            }

            _context.Kmetije.Remove(kmetija);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KmetijaExists(int id)
        {
            return (_context.Kmetije?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
