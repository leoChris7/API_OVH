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
    public class UniteCapteurController : ControllerBase
    {
        private readonly IUniteCapteurRepository<UniteCapteur> dataRepository;

        [ActivatorUtilitiesConstructor]
        public UniteCapteurController(IUniteCapteurRepository<UniteCapteur> manager)
        {
            dataRepository = manager;
        }

        // POST: api/Unites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UniteCapteur>> PostUniteCapteur(UniteCapteur uniteCapteur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(uniteCapteur);

            return CreatedAtAction("GetUniteCapteurById", new { id = uniteCapteur.IdUnite }, uniteCapteur);
        }

        // DELETE: api/Unites/5
        [HttpDelete("{idCapt}-{idUnite}")]
        public async Task<IActionResult> DeleteUnite(int idCapt, int idUnite)
        {
            var liaison = await dataRepository.GetByIdAsync(idCapt, idUnite);
            if (liaison == null)
            {
                return NotFound("delete liaison Unite Capteur non trouvé");
            }

            await dataRepository.DeleteAsync(liaison.Value);

            return NoContent();
        }
    }
}
