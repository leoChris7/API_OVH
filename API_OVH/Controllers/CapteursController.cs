using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.EntityFramework;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapteursController : ControllerBase
    {
        private readonly SAE5_BD_OVH_DbContext _context;

        public CapteursController(SAE5_BD_OVH_DbContext context)
        {
            _context = context;
        }

        // GET: api/Capteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Capteur>>> GetCapteurs()
        {
            return await _context.Capteurs.ToListAsync();
        }

        // GET: api/Capteurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Capteur>> GetCapteur(int id)
        {
            var capteur = await _context.Capteurs.FindAsync(id);

            if (capteur == null)
            {
                return NotFound();
            }

            return capteur;
        }

        // PUT: api/Capteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCapteur(int id, Capteur capteur)
        {
            if (id != capteur.IdCapteur)
            {
                return BadRequest();
            }

            _context.Entry(capteur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CapteurExists(id))
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

        // POST: api/Capteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Capteur>> PostCapteur(Capteur capteur)
        {
            _context.Capteurs.Add(capteur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCapteur", new { id = capteur.IdCapteur }, capteur);
        }

        // DELETE: api/Capteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapteur(int id)
        {
            var capteur = await _context.Capteurs.FindAsync(id);
            if (capteur == null)
            {
                return NotFound();
            }

            _context.Capteurs.Remove(capteur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CapteurExists(int id)
        {
            return _context.Capteurs.Any(e => e.IdCapteur == id);
        }
    }
}
