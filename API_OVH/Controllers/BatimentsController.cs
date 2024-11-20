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

namespace API_OVH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatimentsController : ControllerBase
    {
        private readonly IDataRepository<Batiment> dataRepository;
        private readonly IMapper mapper;

        [ActivatorUtilitiesConstructor]
        public BatimentsController(IDataRepository<Batiment> manager, IMapper mapper)
        {
            dataRepository = manager;
            this.mapper = mapper;
        }

        // GET: api/Batiments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batiment>>> GetBatiment()
        {
            return await dataRepository.GetAllAsync();
        }

        // GET: api/Batiments/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Batiment>> GetBatimentById(int id)
        {
            var leBatiment = await dataRepository.GetByIdAsync(id);

            if (leBatiment == null)
            {
                return NotFound("getbatimentbyid: le batiment n'a pas été trouvé.");
            }

            return leBatiment;
        }

        // GET: api/Batiments/5
        [HttpGet("GetByString/{str}")]
        public async Task<ActionResult<Batiment>> GetBatimentById(string str)
        {
            var leBatiment = await dataRepository.GetByStringAsync(str);

            if (leBatiment == null)
            {
                return NotFound("getbatimentbystr: le batiment n'a pas été trouvé");
            }

            return leBatiment;
        }

        // PUT: api/Batiments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatiment(int id, Batiment batiment)
        {
            if (id != batiment.IdBatiment)
            {
                return BadRequest("Id ne correspondent pas");
            }

            var leBatiment = dataRepository.GetByIdAsync(id);

            if (leBatiment == null)
            {
                return NotFound("Id incorrect: Le batiment na pas été trouvé");
            }

            await dataRepository.UpdateAsync(leBatiment.Result.Value, batiment);
            return NoContent();
        }

        // POST: api/Batiments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Batiment>> PostBatiment(Batiment batiment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await dataRepository.AddAsync(batiment);

            return CreatedAtAction("GetMarqueById", new { id = batiment.IdBatiment }, batiment);
        }

        // DELETE: api/Batiments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatiment(int id)
        {
            var leBatiment = await dataRepository.GetByIdAsync(id);
            if (leBatiment.Value == null)
            {
                return NotFound("delete batiment: batiment non trouvé");
            }

            await dataRepository.DeleteAsync(leBatiment.Value);

            return NoContent();
        }
    }
}
