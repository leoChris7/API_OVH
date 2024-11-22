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
    public class ValeurEquipementController : ControllerBase
    {
        private readonly IDataRepository<ValeurEquipement> dataRepository;
        private readonly IMapper mapper;

        public ValeurEquipementController(IMapper mapper, IDataRepository<ValeurEquipement> dataRepo)
        {
            dataRepository = dataRepo;
            this.mapper = mapper;
        }

        // GET: api/ValeurEquipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValeurEquipement>>> GetValeurEquipements()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/ValeurEquipements/5
        [HttpGet("(GetById/{id})")]
        public async Task<ActionResult<ValeurEquipement>> GetValeurEquipementById(int id)
        {
            var ValeurEquipement = await dataRepository.GetByIdAsync(id);

            if (ValeurEquipement == null)
            {
                return NotFound("GetValeurEquipementById: la caractérisitique n'a pas été trouvé.");
            }

            return ValeurEquipement;
        }

        // PUT: api/ValeurEquipements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValeurEquipement(int id, ValeurEquipement ValeurEquipement)
        {
            if (id != ValeurEquipement.IdCaracteristique)
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

        // POST: api/ValeurEquipements
        [HttpPost]
        public async Task<ActionResult<ValeurEquipement>> PostValeurEquipement(ValeurEquipement ValeurEquipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(ValeurEquipement);

            return CreatedAtAction("GetValeurEquipement", new { id = ValeurEquipement.IdCaracteristique }, ValeurEquipement);
        }

        // DELETE: api/ValeurEquipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValeurEquipement(int id)
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
