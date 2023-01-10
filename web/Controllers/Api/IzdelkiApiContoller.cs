using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Filters;

namespace web.Controllers_Api
{
    [Route("api/v1/Izdelki")]
    [ApiController]
    [ApiKeyAuth]
    public class IzdelkiApiContoller : ControllerBase
    {
        private readonly TrgovinaContext _context;

        public IzdelkiApiContoller(TrgovinaContext context)
        {
            _context = context;
        }

        // GET: api/IzdelkiApiContoller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Izdelek>>> GetIzdelki()
        {
          if (_context.Izdelki == null)
          {
              return NotFound();
          }
            return await _context.Izdelki.ToListAsync();
        }

        // GET: api/IzdelkiApiContoller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Izdelek>> GetIzdelek(int id)
        {
          if (_context.Izdelki == null)
          {
              return NotFound();
          }
            var izdelek = await _context.Izdelki.FindAsync(id);

            if (izdelek == null)
            {
                return NotFound();
            }

            return izdelek;
        }

        // PUT: api/IzdelkiApiContoller/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIzdelek(int id, Izdelek izdelek)
        {
            if (id != izdelek.IzdelekID)
            {
                return BadRequest();
            }

            _context.Entry(izdelek).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IzdelekExists(id))
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

        // POST: api/IzdelkiApiContoller
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Izdelek>> PostIzdelek(Izdelek izdelek)
        {
          if (_context.Izdelki == null)
          {
              return Problem("Entity set 'TrgovinaContext.Izdelki'  is null.");
          }
            _context.Izdelki.Add(izdelek);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIzdelek", new { id = izdelek.IzdelekID }, izdelek);
        }

        // DELETE: api/IzdelkiApiContoller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIzdelek(int id)
        {
            if (_context.Izdelki == null)
            {
                return NotFound();
            }
            var izdelek = await _context.Izdelki.FindAsync(id);
            if (izdelek == null)
            {
                return NotFound();
            }

            _context.Izdelki.Remove(izdelek);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IzdelekExists(int id)
        {
            return (_context.Izdelki?.Any(e => e.IzdelekID == id)).GetValueOrDefault();
        }
    }
}
