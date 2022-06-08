using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GSF.Net.Smtp;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using net6d1.Models;

namespace net6d1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class McihazsController : ControllerBase
    {
        private readonly teknikContext _context;
   

        public McihazsController(teknikContext context)
        {
            _context = context;
        }

        // GET: api/Mcihazs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mcihaz>>> GetMcihazs()
        {
          if (_context.Mcihazs == null)
          {
              return NotFound();
          }
            return await _context.Mcihazs.ToListAsync();
        }

        // GET: api/Mcihazs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mcihaz>> GetMcihaz(int id)
        {
          if (_context.Mcihazs == null)
          {
              return NotFound();
          }
            var mcihaz = await _context.Mcihazs.FindAsync(id);

            if (mcihaz == null)
            {
                return NotFound();
            }

            return mcihaz;
        }

        // PUT: api/Mcihazs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMcihaz(int id, Mcihaz mcihaz)
        {
            string a = "";
            if (id != mcihaz.Id)
            {
                return BadRequest();
            }

            if (mcihaz.Durum == null)
            {
                mcihaz.Durum = 1;
            }
            if (mcihaz.Durum == 2)
            {
                a = " tamire alınmıştır.";
            }
            else if (mcihaz.Durum == 3)
            {
                a = " beklemeye alınmıştır sizinle en kısa sürede iletişim kurulacaktır";
            }
            else if (mcihaz.Durum == 1002)
            {
                a = "'ın tamiri bitmiştir teslim alabilirsiniz.";
            }
            string mail = mcihaz.Mail;
            string adsoyad = mcihaz.AdSoyad;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bilgilendirme", "projedenemeapi@gmail.com"));
            message.To.Add(new MailboxAddress(adsoyad, mail));
            message.Subject = "Teknik Servis Bilgilendirme";
            message.Body = new TextPart("plain")
            {
                Text = "Sayın " + adsoyad + " cihazınız " + a
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("projedenemeapi@gmail.com", "visualstudio");
                client.Send(message);
                client.Disconnect(true);
            }
            _context.Entry(mcihaz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!McihazExists(id))
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

        // POST: api/Mcihazs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mcihaz>> PostMcihaz(Mcihaz mcihaz)
        {
          
                if (_context.Mcihazs == null)
                {
                    return Problem("Entity set 'teknikContext.Mcihazs'  is null.");
                }
            mcihaz.Vtarih = DateTime.Now;
            mcihaz.Durum = 1;
            _context.Mcihazs.Add(mcihaz);
            await _context.SaveChangesAsync();

            string mail = mcihaz.Mail;
            string adsoyad = mcihaz.AdSoyad;
            string cihaz = mcihaz.Cihaz;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Deneme Deneme", "projedenemeapi@gmail.com"));
            message.To.Add(new MailboxAddress(adsoyad, mail));
            message.Subject = "Teknik Servis Bilgilendirme";
            message.Body = new TextPart("plain")
            {
                Text = "Sayın " + adsoyad + " cihazınız sıraya alınmıştır en kısa sürede işleme alınacaktır\n" +
                "Cihaz: " + cihaz + "\n" +
                "Getirilen: " + mcihaz.Getirilen + "\n" +
                "İslem: " + mcihaz.DetayIslem + "\n"
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("projedenemeapi@gmail.com", "visualstudio");
                client.Send(message);
                client.Disconnect(true);
            }

            return CreatedAtAction("GetMcihaz", new { id = mcihaz.Id }, mcihaz);
        }

        // DELETE: api/Mcihazs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMcihaz(int id)
        {
            if (_context.Mcihazs == null)
            {
                return NotFound();
            }
            var mcihaz = await _context.Mcihazs.FindAsync(id);
            if (mcihaz == null)
            {
                return NotFound();
            }

            _context.Mcihazs.Remove(mcihaz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool McihazExists(int id)
        {
            return (_context.Mcihazs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
