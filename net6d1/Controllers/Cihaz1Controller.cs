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
    public class Cihaz1Controller : Controller
    {
        private readonly d1Context _context;

        public Cihaz1Controller(d1Context context)
        {
            _context = context;
        }

        // GET: Cihaz1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cihaz1>>> GetCihaz1()
        {
            if (_context.Cihaz1s == null)
            {
                return NotFound();
            }
            return await _context.Cihaz1s.ToListAsync();    
        }
    }
}
