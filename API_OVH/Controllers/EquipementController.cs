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
using API_OVH.Models.DTO;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipementController : ControllerBase
    {
        private readonly IEquipementRepository<Equipement, EquipementDTO, EquipementDetailDTO, EquipementSansNavigationDTO> dataRepository;

        [ActivatorUtilitiesConstructor]
        public EquipementController(IEquipementRepository<Equipement, EquipementDTO, EquipementDetailDTO, EquipementSansNavigationDTO> manager)
        {
            dataRepository = manager;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipementDTO>>> GetEquipements()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Equipements/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<EquipementDetailDTO>> GetEquipementById(int id)
        {
            var equipement = await dataRepository.GetByIdAsync(id);

            if (equipement == null)
            {
                return NotFound("getEquipementByID: l'équipement n'a pas été trouvé.");
            }

            return equipement;
        }

        // GET: api/Equipements/5
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<EquipementDetailDTO>> GetEquipementByString(string name)
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

            var equipement = dataRepository.GetByIdWithoutDTOAsync(id);

            if (equipement.Result == null)
            {
                return NotFound("Id incorrect: L'équipement na pas été trouvé");
            }

            await dataRepository.UpdateAsync(equipement.Result.Value, Equipement);
            return NoContent();
        }

        // POST: api/Equipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EquipementSansNavigationDTO>> PostEquipement(EquipementSansNavigationDTO Equipement)
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
            var equipement = await dataRepository.GetByIdWithoutDTOAsync(id);
            if (equipement == null)
            {
                return NotFound("delete Equipement: Equipement non trouvé");
            }

            await dataRepository.DeleteAsync(equipement.Value);

            return NoContent();
        }
    }
}
