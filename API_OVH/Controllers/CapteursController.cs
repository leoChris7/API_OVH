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
    public class CapteursController : ControllerBase
    {
        private readonly IDataRepository<Capteur> dataRepository;
        private readonly IMapper mapper;

        public CapteursController(IMapper mapper, IDataRepository<Capteur> dataRepo)
        {
            dataRepository = dataRepo;
            mapper = mapper;
        }

        // GET: api/Capteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Capteur>>> GetCapteurs()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Capteurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Capteur>> GetCapteurById(int id)
        {
            var leCapteur = await dataRepository.GetByIdAsync(id);

            if (leCapteur == null)
            {
                return NotFound("getCapteurById: le capteur n'a pas été trouvé.");
            }

            return leCapteur;
        }

        // PUT: api/Capteurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCapteur(int id, Capteur capteur)
        {
            if (id != capteur.IdCapteur)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var capteurToUpdate = dataRepository.GetByIdAsync(id);

            if (capteurToUpdate == null)
            {
                return NotFound("Id incorrect: Le capteur na pas été trouvé");
            }

            await dataRepository.UpdateAsync(capteurToUpdate.Result.Value, capteur);
            return NoContent();
        }

        // POST: api/Capteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Capteur>> PostCapteur(Capteur capteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(capteur);

            return CreatedAtAction("GetById", new { id = capteur.IdCapteur }, capteur);
        }

        // DELETE: api/Capteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapteur(int id)
        {
            var leCapteur = await dataRepository.GetByIdAsync(id);
            if (leCapteur == null)
            {
                return NotFound("delete capteur: catpeur non trouvé");
            }

            await dataRepository.DeleteAsync(leCapteur.Value);
            return NoContent();
        }
    }
}
