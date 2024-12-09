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
    public class MursController : ControllerBase
    {
        private readonly IMurRepository<Mur, MurDTO, MurSansNavigationDTO> dataRepository;

        [ActivatorUtilitiesConstructor]
        public MursController(IMurRepository<Mur, MurDTO, MurSansNavigationDTO> manager)
        {
            dataRepository = manager;
        }

        // GET: api/Murs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MurDTO>>> GetMurs()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Murs/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Mur>> GetMurById(int id)
        {
            var leMur = await dataRepository.GetByIdAsync(id);

            if (leMur == null)
            {
                return NotFound("GetMurById: le Mur n'a pas été trouvé.");
            }

            return leMur;
        }

        // PUT: api/Murs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMur(int id, Mur Mur)
        {
            if (id != Mur.IdMur)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var leMur = dataRepository.GetByIdAsync(id);

            if (leMur.Result == null)
            {
                return NotFound("Id incorrect: Le Mur na pas été trouvé");
            }

            await dataRepository.UpdateAsync(leMur.Result.Value, Mur);
            return NoContent();
        }

        // POST: api/Murs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MurSansNavigationDTO>> PostMur(MurSansNavigationDTO mur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(mur);

            return CreatedAtAction("GetMurById", new { id = mur.IdMur }, mur);
        }

        // DELETE: api/Murs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMur(int id)
        {
            var leMur = await dataRepository.GetByIdAsync(id);
            if (leMur == null)
            {
                return NotFound("delete Mur: Mur non trouvé");
            }

            await dataRepository.DeleteAsync(leMur.Value);

            return NoContent();
        }
    }
}
