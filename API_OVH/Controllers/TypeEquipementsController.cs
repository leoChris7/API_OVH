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
    public class TypeEquipementsController : ControllerBase
    {
        private readonly ITypeEquipementRepository<TypeEquipement, TypeEquipementDTO, TypeEquipementDetailDTO> dataRepository;

        [ActivatorUtilitiesConstructor]
        public TypeEquipementsController(ITypeEquipementRepository<TypeEquipement, TypeEquipementDTO, TypeEquipementDetailDTO> manager)
        {
            dataRepository = manager;
        }

        // GET: api/TypeEquipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeEquipementDTO>>> GetTypesEquipement()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/TypeEquipements/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TypeEquipementDetailDTO>> GetTypeEquipementById(int id)
        {
            var leTypeEquipement = await dataRepository.GetByIdAsync(id);

            if (leTypeEquipement == null)
            {
                return NotFound("getTypeEquipementbyid: le TypeEquipement n'a pas été trouvé.");
            }

            return leTypeEquipement;
        }

        // GET: api/TypeEquipements/TETRAS
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<TypeEquipementDetailDTO>> GetTypeEquipementByName(string name)
        {
            var leTypeEquipement = await dataRepository.GetByStringAsync(name);

            if (leTypeEquipement == null)
            {
                return NotFound("GetTypeEquipementbyName: le TypeEquipement n'a pas été trouvé");
            }

            return leTypeEquipement;
        }

        // PUT: api/TypeEquipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeEquipement(int id, TypeEquipementDTO TypeEquipement)
        {
            if (id != TypeEquipement.IdTypeEquipement)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var leTypeEquipement = dataRepository.GetByIdWithoutDTOAsync(id);

            if (leTypeEquipement.Result == null)
            {
                return NotFound("Id incorrect: Le TypeEquipement na pas été trouvé");
            }


            await dataRepository.UpdateAsync(leTypeEquipement.Result.Value, TypeEquipement);
            return NoContent();
        }

        // POST: api/TypeEquipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeEquipementDTO>> PostTypeEquipement(TypeEquipementDTO TypeEquipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(TypeEquipement);

            return CreatedAtAction("GetTypeEquipementById", new { id = TypeEquipement.IdTypeEquipement }, TypeEquipement);
        }

        // DELETE: api/TypeEquipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeEquipement(int id)
        {
            var leTypeEquipement = await dataRepository.GetByIdWithoutDTOAsync(id);

            if (leTypeEquipement == null)
            {
                return NotFound("delete TypeEquipement: TypeEquipement non trouvé");
            }

            await dataRepository.DeleteAsync(leTypeEquipement.Value);

            return NoContent();
        }
    }
}
