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
using static API_OVH.Models.Repository.IUniteRepository;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitesController : ControllerBase
    {
        private readonly IUniteRepository<Unite, UniteDTO, UniteDetailDTO> dataRepository;

        [ActivatorUtilitiesConstructor]
        public UnitesController(IUniteRepository<Unite, UniteDTO, UniteDetailDTO> manager)
        {
            dataRepository = manager;
        }

        // GET: api/Unites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniteDTO>>> GetUnite()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Unites/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<UniteDetailDTO>> GetUniteById(int id)
        {
            var leUnite = await dataRepository.GetByIdAsync(id);

            if (leUnite == null)
            {
                return NotFound("getUnitebyid: le Unite n'a pas été trouvé.");
            }

            return leUnite;
        }

        // GET: api/Unites/Centimètre
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<UniteDetailDTO>> GetUniteByName(string name)
        {
            var leUnite = await dataRepository.GetByStringAsync(name);

            if (leUnite == null)
            {
                return NotFound("GetUnitebyName: le Unite n'a pas été trouvé");
            }

            return leUnite;
        }

        // GET: api/Unites/cm
        [HttpGet("GetBySigle/{sigle}")]
        public async Task<ActionResult<UniteDetailDTO>> GetUniteBySigle(string sigle)
        {
            var leUnite = await dataRepository.GetBySigleAsync(sigle);

            if (leUnite == null)
            {
                return NotFound("GetUnitebyName: l'Unite n'a pas été trouvé");
            }

            return leUnite;
        }

        // PUT: api/Unites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnite(int id, Unite Unite)
        {
            if (id != Unite.IdUnite)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var leUnite = dataRepository.GetByIdWithoutDTOAsync(id);

            if (leUnite.Result == null)
            {
                return NotFound("Id incorrect: Le Unite na pas été trouvé");
            }

            await dataRepository.UpdateAsync(leUnite.Result.Value, Unite);
            return NoContent();
        }

        // POST: api/Unites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unite>> PostUnite(UniteDTO Unite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(Unite);

            return CreatedAtAction("GetUniteById", new { id = Unite.IdUnite }, Unite);
        }

        // DELETE: api/Unites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnite(int id)
        {
            var leUnite = await dataRepository.GetByIdWithoutDTOAsync(id);
            if (leUnite == null)
            {
                return NotFound("delete Unite: Unite non trouvé");
            }

            await dataRepository.DeleteAsync(leUnite.Value);

            return NoContent();
        }
    }
}
