using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net6d1.Models;

namespace net6d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DurumsController : ControllerBase
    {
        private readonly teknikContext _context;

        public DurumsController(teknikContext context)
        {
            _context = context;
        }

        // GET: api/Durums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Durum>>> GetDurums()
        {
          if (_context.Durums == null)
          {
              return NotFound();
          }
            return await _context.Durums.ToListAsync();
        }

        // GET: api/Durums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Durum>> GetDurum(int id)
        {
          if (_context.Durums == null)
          {
              return NotFound();
          }
            var durum = await _context.Durums.FindAsync(id);

            if (durum == null)
            {
                return NotFound();
            }

            return durum;
        }

        // PUT: api/Durums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDurum(int id, Durum durum)
        {
            if (id != durum.Id)
            {
                return BadRequest();
            }

            _context.Entry(durum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DurumExists(id))
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

        // POST: api/Durums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Durum>> PostDurum(Durum durum)
        {
          if (_context.Durums == null)
          {
              return Problem("Entity set 'teknikContext.Durums'  is null.");
          }
            _context.Durums.Add(durum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDurum", new { id = durum.Id }, durum);
        }

        // DELETE: api/Durums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDurum(int id)
        {
            if (_context.Durums == null)
            {
                return NotFound();
            }
            var durum = await _context.Durums.FindAsync(id);
            if (durum == null)
            {
                return NotFound();
            }

            _context.Durums.Remove(durum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DurumExists(int id)
        {
            return (_context.Durums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
