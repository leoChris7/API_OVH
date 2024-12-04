using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using AutoMapper;
using API_OVH.Models.Manager;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipementController : ControllerBase
    {
        private readonly IDataRepository<Equipement> dataRepository;

        [ActivatorUtilitiesConstructor]
        public EquipementController(IDataRepository<Equipement> manager)
        {
            dataRepository = manager;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipement>>> GetEquipement()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Equipements/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Equipement>> GetEquipementById(int id)
        {
            var leEquipement = await dataRepository.GetByIdAsync(id);

            if (leEquipement == null)
            {
                return NotFound("getEquipementbyid: l'équipement n'a pas été trouvé.");
            }

            return leEquipement;
        }

        // GET: api/Equipements/5
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<Equipement>> GetEquipementByString(string name)
        {
            var leEquipement = await dataRepository.GetByStringAsync(name);
            
            if (leEquipement == null)
            {
                return NotFound("GetEquipementByName: l'équipement n'a pas été trouvé");
            }

            return leEquipement;
        }

        // PUT: api/Equipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipement(int id, Equipement Equipement)
        {
            if (id != Equipement.IdEquipement)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var leEquipement = dataRepository.GetByIdAsync(id);

            if (leEquipement == null)
            {
                return NotFound("Id incorrect: L'équipement na pas été trouvé");
            }

            await dataRepository.UpdateAsync(leEquipement.Result.Value, Equipement);
            return NoContent();
        }

        // POST: api/Equipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Equipement>> PostEquipement(Equipement Equipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(Equipement);

            return CreatedAtAction("GetEquipementById", new { id = Equipement.IdEquipement }, Equipement);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipement(int id)
        {
            var leEquipement = await dataRepository.GetByIdAsync(id);
            if (leEquipement.Value == null)
            {
                return NotFound("delete Equipement: Equipement non trouvé");
            }

            await dataRepository.DeleteAsync(leEquipement.Value);

            return NoContent();
        }
    }
}
