using Microsoft.AspNetCore.Mvc;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using API_OVH.Models.DTO;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniteCapteurController : ControllerBase
    {
        private readonly IUniteCapteurRepository<UniteCapteur, UniteCapteurSansNavigationDTO, UniteCapteurDetailDTO> dataRepository;

        [ActivatorUtilitiesConstructor]
        public UniteCapteurController(IUniteCapteurRepository<UniteCapteur, UniteCapteurSansNavigationDTO, UniteCapteurDetailDTO> manager)
        {
            dataRepository = manager;
        }

        // POST: api/Unites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UniteCapteurSansNavigationDTO>> PostUniteCapteur(UniteCapteurSansNavigationDTO uniteCapteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(uniteCapteur);

            return Created("", new { idUnite = uniteCapteur.IdUnite, idCapteur = uniteCapteur.IdCapteur });
        }

        // DELETE: api/Unites/5
        [HttpDelete("{idCapt}-{idUnite}")]
        public async Task<IActionResult> DeleteUnite(int idCapt, int idUnite)
        {
            var liaison = await dataRepository.GetByIdWithoutDTOAsync(idCapt, idUnite);
            if (liaison == null)
            {
                return NotFound("delete liaison Unite Capteur non trouvé");
            }

            await dataRepository.DeleteAsync(liaison.Value);

            return NoContent();
        }
    }
}
