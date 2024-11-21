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

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaracteristiqueEquipementController : ControllerBase
    {
        private readonly IDataRepository<CaracteristiqueEquipement> dataRepository;
        private readonly IMapper mapper;

        public CaracteristiqueEquipementController(IMapper mapper, IDataRepository<CaracteristiqueEquipement> dataRepo)
        {
            dataRepository = dataRepo;
            mapper = mapper;
        }

        // GET: api/CaracteristiqueEquipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaracteristiqueEquipement>>> GetCaracteristiqueEquipements()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/CaracteristiqueEquipements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CaracteristiqueEquipement>> GetCaracteristiqueEquipement(int id)
        {
            var caracteristiqueEquipement = await dataRepository.GetByIdAsync(id);

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
            if (id != caracteristiqueEquipement.IdCaracteristique)
            {
                return BadRequest();
            }

            var caracteristiqueAModifier = dataRepository.GetByIdAsync(id);

            if (caracteristiqueAModifier == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/CaracteristiqueEquipements
        [HttpPost]
        public async Task<ActionResult<CaracteristiqueEquipement>> PostCaracteristiqueEquipement(CaracteristiqueEquipement caracteristiqueEquipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(caracteristiqueEquipement);

            return CreatedAtAction("GetCaracteristiqueEquipement", new { id = caracteristiqueEquipement.IdCaracteristique }, caracteristiqueEquipement);
        }

        // DELETE: api/CaracteristiqueEquipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaracteristiqueEquipement(int id)
        {
            var laCaracteristique = await dataRepository.GetByIdAsync(id);
            if (laCaracteristique.Value == null)
            {
                return NotFound("delete caracteristique: caracteristique non trouvé");
            }

            await dataRepository.DeleteAsync(laCaracteristique.Value);

            return NoContent();
        }
    }
}
