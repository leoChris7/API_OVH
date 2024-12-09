using Microsoft.AspNetCore.Mvc;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using API_OVH.Models.DTO;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SallesController : ControllerBase
    {
        private readonly ISalleRepository<Salle, SalleSansNavigationDTO, SalleDTO, SalleDetailDTO> dataRepository;

        [ActivatorUtilitiesConstructor]
        public SallesController(ISalleRepository<Salle, SalleSansNavigationDTO, SalleDTO, SalleDetailDTO> manager)
        {
            dataRepository = manager;
        }

        // GET: api/Salles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalleDTO>>> GetSalles()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Salles/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<SalleDetailDTO>> GetSalleById(int id)
        {
            var leSalle = await dataRepository.GetByIdAsync(id);

            if (leSalle == null)
            {
                return NotFound("getSallebyid: le Salle n'a pas été trouvé.");
            }

            return leSalle;
        }

        // GET: api/Salles/D101
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<SalleDetailDTO>> GetSalleByName(string name)
        {
            var leSalle = await dataRepository.GetByStringAsync(name);

            if (leSalle == null)
            {
                return NotFound("GetSallebyName: le Salle n'a pas été trouvé");
            }

            return leSalle;
        }

        // PUT: api/Salles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalle(int id, Salle Salle)
        {
            if (id != Salle.IdSalle)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var leSalle = dataRepository.GetByIdWithoutDTOAsync(id);

            if (leSalle.Result == null)
            {
                return NotFound("Id incorrect: Le Salle na pas été trouvé");
            }

            await dataRepository.UpdateAsync(leSalle.Result.Value, Salle);
            return NoContent();
        }

        // POST: api/Salles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalleSansNavigationDTO>> PostSalle(SalleSansNavigationDTO Salle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(Salle);

            return CreatedAtAction("GetSalleById", new { id = Salle.IdSalle }, Salle);
        }

        // DELETE: api/Salles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalle(int id)
        {
            var leSalle = await dataRepository.GetByIdWithoutDTOAsync(id);
            if (leSalle == null)
            {
                return NotFound("delete Salle: Salle non trouvé");
            }

            await dataRepository.DeleteAsync(leSalle.Value);

            return NoContent();
        }
    }
}
