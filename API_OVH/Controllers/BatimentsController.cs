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
    public class BatimentsController : ControllerBase
    {
        private readonly SAE5_BD_OVH_DbContext _context;

        public BatimentsController(SAE5_BD_OVH_DbContext context)
        {
            _context = context;
        }

        // GET: api/Batiments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batiment>>> GetBatiment()
        {
            return await _context.Batiment.ToListAsync();
        }

        // GET: api/Batiments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Batiment>> GetBatiment(int id)
        {
            var batiment = await _context.Batiment.FindAsync(id);

            if (batiment == null)
            {
                return NotFound();
            }

            return batiment;
        }

        // PUT: api/Batiments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatiment(int id, Batiment batiment)
        {
            if (id != batiment.Idbatiment)
            {
                return BadRequest();
            }

            _context.Entry(batiment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatimentExists(id))
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

        // POST: api/Batiments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Batiment>> PostBatiment(Batiment batiment)
        {
            _context.Batiment.Add(batiment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBatiment", new { id = batiment.Idbatiment }, batiment);
        }

        // DELETE: api/Batiments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatiment(int id)
        {
            var batiment = await _context.Batiment.FindAsync(id);
            if (batiment == null)
            {
                return NotFound();
            }

            _context.Batiment.Remove(batiment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatimentExists(int id)
        {
            return _context.Batiment.Any(e => e.Idbatiment == id);
        }
    }
}
