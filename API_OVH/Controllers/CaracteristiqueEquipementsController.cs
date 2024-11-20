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
    public class CaracteristiqueEquipementsController : ControllerBase
    {
        private readonly SAE5_BD_OVH_DbContext _context;

        public CaracteristiqueEquipementsController(SAE5_BD_OVH_DbContext context)
        {
            _context = context;
        }

        // GET: api/CaracteristiqueEquipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaracteristiqueEquipement>>> GetCaracteristiqueEquipements()
        {
            return await _context.CaracteristiqueEquipements.ToListAsync();
        }

        // GET: api/CaracteristiqueEquipements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaracteristiqueEquipement>> GetCaracteristiqueEquipement(int id)
        {
            var caracteristiqueEquipement = await _context.CaracteristiqueEquipements.FindAsync(id);

            if (caracteristiqueEquipement == null)
            {
                return NotFound();
            }

            return caracteristiqueEquipement;
        }

        // PUT: api/CaracteristiqueEquipements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCaracteristiqueEquipement(int id, CaracteristiqueEquipement caracteristiqueEquipement)
        {
            if (id != caracteristiqueEquipement.Idcaracteristique)
            {
                return BadRequest();
            }

            _context.Entry(caracteristiqueEquipement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CaracteristiqueEquipements.Any(e => e.Idcaracteristique == id))
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

        // POST: api/CaracteristiqueEquipements
        [HttpPost]
        public async Task<ActionResult<CaracteristiqueEquipement>> PostCaracteristiqueEquipement(CaracteristiqueEquipement caracteristiqueEquipement)
        {
            _context.CaracteristiqueEquipements.Add(caracteristiqueEquipement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCaracteristiqueEquipement", new { id = caracteristiqueEquipement.Idcaracteristique }, caracteristiqueEquipement);
        }

        // DELETE: api/CaracteristiqueEquipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaracteristiqueEquipement(int id)
        {
            var caracteristiqueEquipement = await _context.CaracteristiqueEquipements.FindAsync(id);
            if (caracteristiqueEquipement == null)
            {
                return NotFound();
            }

            _context.CaracteristiqueEquipements.Remove(caracteristiqueEquipement);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
