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

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController : ControllerBase
    {
        private readonly IReadOnlyDataRepository<Direction> directionManager;
        private readonly IMapper mapper;

        [ActivatorUtilitiesConstructor]
        public DirectionController(DirectionManager manager, IMapper mapper)
        {
            directionManager = manager;
            this.mapper = mapper;
        }

        // GET: api/Directions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Direction>>> GetDirections()
        {
            return await directionManager.GetAllAsync();
        }

        // GET: api/Directions/5
        [HttpGet("(GetById/{id})")]
        public async Task<ActionResult<Direction>> GetDirection(int id)
        {
            var direction = await directionManager.GetByIdAsync(id);

            if (direction == null)
            {
                return NotFound();
            }

            return direction;
        }

        // GET: api/Directions/S
        [HttpGet("(GetByLetters/{letters})")]
        public async Task<ActionResult<Direction>> GetDirectionByLetters(String letters)
        {
            var lettreDirection = await directionManager.GetByStringAsync(letters);

            if (lettreDirection == null)
            {
                return NotFound();
            }

            return lettreDirection;
        }

    }
}
