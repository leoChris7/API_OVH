﻿using System;
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

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SallesController : ControllerBase
    {
        private readonly IDataRepository<Salle> dataRepository;
        private readonly IMapper mapper;

        [ActivatorUtilitiesConstructor]
        public SallesController(IDataRepository<Salle> manager, IMapper mapper)
        {
            dataRepository = manager;
            this.mapper = mapper;
        }

        // GET: api/Salles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salle>>> GetSalle()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Salles/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Salle>> GetSalleById(int id)
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
        public async Task<ActionResult<Salle>> GetSalleByName(string name)
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

            var leSalle = dataRepository.GetByIdAsync(id);

            if (leSalle == null)
            {
                return NotFound("Id incorrect: Le Salle na pas été trouvé");
            }

            await dataRepository.UpdateAsync(leSalle.Result.Value, Salle);
            return NoContent();
        }

        // POST: api/Salles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salle>> PostSalle(Salle Salle)
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
            var leSalle = await dataRepository.GetByIdAsync(id);
            if (leSalle.Value == null)
            {
                return NotFound("delete Salle: Salle non trouvé");
            }

            await dataRepository.DeleteAsync(leSalle.Value);

            return NoContent();
        }
    }
}