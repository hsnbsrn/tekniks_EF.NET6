using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net6d1.Model;

namespace net6d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PersonelController : ControllerBase
    {
        private readonly d1Context _context;

        public PersonelController(d1Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personel>>> GetPersonel()
        {
            return await _context.Personels.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Personel>> GetPersonel(int id)
        {
            var personel = await _context.Personels.FindAsync(id);

            if (personel == null)
            {
                return NotFound();
            }

            return personel;
        }
        [HttpPost]
        public async Task<ActionResult<Personel>> PostPersonel(Personel personel)
        {
            personel.Aktifmi = false;
            _context.Personels.Add(personel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonel", new { id = personel.Id }, personel);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonel(int id, Personel personel)
        {
            if (id != personel.Id)
            {
                return BadRequest();
            }

            _context.Entry(personel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonelExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonel(int id)
        {

            if (_context.Personels == null)
            {
                return NotFound();
            }
            var personel = await _context.Personels.FindAsync(id);
            if (personel == null)
            {
                return NotFound();
            }

            _context.Personels.Remove(personel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonelExists(int id)
        {
            return (_context.Personels?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
