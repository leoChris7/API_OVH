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
    public class TypeSallesController : ControllerBase
    {
        private readonly ITypeSalleRepository<TypeSalle, TypeSalleDTO, TypeSalleDetailDTO> dataRepository;

        [ActivatorUtilitiesConstructor]
        public TypeSallesController(ITypeSalleRepository<TypeSalle, TypeSalleDTO, TypeSalleDetailDTO> manager)
        {
            dataRepository = manager;
        }

        // GET: api/TypeSalles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeSalleDTO>>> GetTypesSalle()
        {
            return await dataRepository.GetAllAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TypeSalleDetailDTO>> GetTypeSalleById(int id)
        {
            var leTypeSalle = await dataRepository.GetByIdAsync(id);

            if (leTypeSalle == null)
            {
                return NotFound("getTypeSallebyid: le TypeSalle n'a pas été trouvé.");
            }

            return leTypeSalle;
        }


        // GET: api/TypeSalles/TETRAS
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<TypeSalleDetailDTO>> GetTypeSalleByName(string name)
        {
            var leTypeSalle = await dataRepository.GetByStringAsync(name);

            if (leTypeSalle == null)
            {
                return NotFound("GetTypeSallebyName: le TypeSalle n'a pas été trouvé");
            }

            return leTypeSalle;
        }

        // PUT: api/TypeSalles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeSalle(int id, TypeSalleDTO TypeSalle)
        {
            if (id != TypeSalle.IdTypeSalle)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var leTypeSalle = dataRepository.GetByIdWithoutDTOAsync(id);

            if (leTypeSalle.Result == null)
            {
                return NotFound("Id incorrect: Le TypeSalle na pas été trouvé");
            }

            await dataRepository.UpdateAsync(leTypeSalle.Result.Value, TypeSalle);
            return NoContent();
        }

        // POST: api/TypeSalles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeSalleDTO>> PostTypeSalle(TypeSalleDTO TypeSalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(TypeSalle);

            return CreatedAtAction("GetTypeSalleById", new { id = TypeSalle.IdTypeSalle }, TypeSalle);
        }

        // DELETE: api/TypeSalles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeSalle(int id)
        {
            var leTypeSalle = await dataRepository.GetByIdWithoutDTOAsync(id);
            if (leTypeSalle == null)
            {
                return NotFound("delete TypeSalle: TypeSalle non trouvé");
            }

            await dataRepository.DeleteAsync(leTypeSalle.Value);

            return NoContent();
        }
    }
}
