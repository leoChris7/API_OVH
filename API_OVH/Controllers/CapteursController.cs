using Microsoft.AspNetCore.Mvc;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using API_OVH.Models.DTO;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapteursController : ControllerBase
    {
        private readonly ICapteurRepository<Capteur, CapteurDTO, CapteurDetailDTO, CapteurSansNavigationDTO> dataRepository;

        public CapteursController(ICapteurRepository<Capteur, CapteurDTO, CapteurDetailDTO, CapteurSansNavigationDTO> dataRepo)
        {
            dataRepository = dataRepo;
        }

        // GET: api/Capteurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CapteurDTO>>> GetCapteurs()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Capteurs/5
        [HttpGet("(GetById/{id})")]
        public async Task<ActionResult<CapteurDetailDTO>> GetCapteurById(int id)
        {
            var leCapteur = await dataRepository.GetByIdAsync(id);

            if (leCapteur == null)
            {
                return NotFound("getCapteurById: le capteur n'a pas été trouvé.");
            }

            return leCapteur;
        }

        // GET: api/Capteurs/5
        [HttpGet("GetByIdWithoutDTO/{id}")]
        public async Task<ActionResult<Capteur>> GetCapteurByIdWithoutDTO(int id)
        {
            var leCapteur = await dataRepository.GetByIdWithoutDTOAsync(id);

            if (leCapteur == null)
            {
                return NotFound("getCapteurById: le capteur n'a pas été trouvé.");
            }

            return leCapteur;
        }


        // GET: api/Capteurs/CapteurCO2
        [HttpGet("(GetByName/{name})")]
        public async Task<ActionResult<CapteurDetailDTO>> GetCapteurByName(String name)
        {
            var leCapteur = await dataRepository.GetByStringAsync(name);

            if (leCapteur == null)
            {
                return NotFound("GetCapteurByName: le capteur n'a pas été trouvé.");
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

            var capteurToUpdate = dataRepository.GetByIdWithoutDTOAsync(id);

            if (capteurToUpdate.Result == null)
            {
                return NotFound("Id incorrect: Le capteur na pas été trouvé");
            }

            await dataRepository.UpdateAsync(capteurToUpdate.Result.Value, capteur);
            return NoContent();
        }

        // POST: api/Capteurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CapteurSansNavigationDTO>> PostCapteur(CapteurSansNavigationDTO capteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(capteur);

            return CreatedAtAction("GetCapteurById", new { id = capteur.IdCapteur }, capteur);
        }

        // DELETE: api/Capteurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapteur(int id)
        {
            var leCapteur = await dataRepository.GetByIdWithoutDTOAsync(id);
            if (leCapteur == null)
            {
                return NotFound("delete capteur: catpeur non trouvé");
            }

            await dataRepository.DeleteAsync(leCapteur.Value);
            return NoContent();
        }
    }
}
