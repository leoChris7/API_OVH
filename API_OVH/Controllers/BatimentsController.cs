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
using API_OVH.Models.DTO;

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatimentsController : ControllerBase
    {
        private readonly IBatimentRepository<Batiment, BatimentDTO, BatimentDetailDTO, BatimentSansNavigationDTO> dataRepository;

        [ActivatorUtilitiesConstructor]
        public BatimentsController(IBatimentRepository<Batiment, BatimentDTO, BatimentDetailDTO, BatimentSansNavigationDTO> manager)
        {
            dataRepository = manager;
        }

        // GET: api/Batiments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatimentDTO>>> GetBatiments()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Batiments/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BatimentDetailDTO>> GetBatimentById(int id)
        {
            var leBatiment = await dataRepository.GetByIdAsync(id);

            if (leBatiment == null)
            {
                return NotFound("getbatimentbyid: le batiment n'a pas été trouvé.");
            }

            return leBatiment;
        }

        // GET: api/Batiments/TETRAS
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<BatimentDetailDTO>> GetBatimentByName(string name)
        {
            var leBatiment = await dataRepository.GetByStringAsync(name);

            if (leBatiment == null)
            {
                return NotFound("GetbatimentbyName: le batiment n'a pas été trouvé");
            }

            return leBatiment;
        }

        // PUT: api/Batiments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatiment(int id, BatimentSansNavigationDTO batiment)
        {
            if (id != batiment.IdBatiment)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var leBatiment = dataRepository.GetByIdWithoutDTOAsync(id);

            if (leBatiment.Result == null)
            {
                return NotFound("Id incorrect: Le batiment na pas été trouvé");
            }

            await dataRepository.UpdateAsync(leBatiment.Result.Value, batiment);
            return NoContent();
        }

        // POST: api/Batiments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BatimentSansNavigationDTO>> PostBatiment(BatimentSansNavigationDTO batiment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(batiment);

            return CreatedAtAction("GetBatimentById", new { id = batiment.IdBatiment }, batiment);
        }

        // DELETE: api/Batiments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatiment(int id)
        {
            var leBatiment = await dataRepository.GetByIdWithoutDTOAsync(id);
            if (leBatiment == null)
            {
                return NotFound("delete batiment: batiment non trouvé");
            }

            await dataRepository.DeleteAsync(leBatiment.Value);

            return NoContent();
        }
    }
}
