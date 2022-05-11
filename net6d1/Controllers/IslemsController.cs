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
    public class IslemsController : ControllerBase
    {
        private readonly teknikContext _context;

        public IslemsController(teknikContext context)
        {
            _context = context;
        }

        // GET: api/Islems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Islem>>> GetIslems()
        {
          if (_context.Islems == null)
          {
              return NotFound();
          }
            return await _context.Islems.ToListAsync();
        }

        // GET: api/Islems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Islem>> GetIslem(int id)
        {
          if (_context.Islems == null)
          {
              return NotFound();
          }
            var islem = await _context.Islems.FindAsync(id);

            if (islem == null)
            {
                return NotFound();
            }

            return islem;
        }

        // PUT: api/Islems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIslem(int id, Islem islem)
        {
            if (id != islem.Id)
            {
                return BadRequest();
            }

            _context.Entry(islem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IslemExists(id))
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

        // POST: api/Islems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Islem>> PostIslem(Islem islem)
        {
          if (_context.Islems == null)
          {
              return Problem("Entity set 'teknikContext.Islems'  is null.");
          }
            _context.Islems.Add(islem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIslem", new { id = islem.Id }, islem);
        }

        // DELETE: api/Islems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIslem(int id)
        {
            if (_context.Islems == null)
            {
                return NotFound();
            }
            var islem = await _context.Islems.FindAsync(id);
            if (islem == null)
            {
                return NotFound();
            }

            _context.Islems.Remove(islem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IslemExists(int id)
        {
            return (_context.Islems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
