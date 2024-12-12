using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Manager;
using AutoMapper;
using API_OVH.Models.Repository;
using API_OVH.Models.DTO;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
        private readonly IDirectionRepository<DirectionDetailDTO, DirectionSansNavigationDTO> directionManager;

        [ActivatorUtilitiesConstructor]
        public DirectionController(IDirectionRepository<DirectionDetailDTO, DirectionSansNavigationDTO> manager)
        {
            directionManager = manager;
        }

        // GET: api/Directions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectionSansNavigationDTO>>> GetDirections()
        {
            return await directionManager.GetAllAsync();
        }

        // GET: api/Directions/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<DirectionDetailDTO>> GetDirection(int id)
        {
            var direction = await directionManager.GetByIdAsync(id);

            if (direction == null)
            {
                return NotFound();
            }

            return direction;
        }

        // GET: api/Directions/69
        [HttpGet("GetByDegre/{degre}")]
        public async Task<ActionResult<DirectionDetailDTO>> GetDirectionByDegre(decimal degre)
        {
            var lettreDirection = await directionManager.GetByDegreAsync(degre);

            if (lettreDirection == null)
            {
                return NotFound();
            }

            return lettreDirection;
        }

    }
}
