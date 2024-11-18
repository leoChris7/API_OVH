using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionProduit_API.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_OVH.Models.EntityFramework;
using AutoMapper;
using API_OVH.Models.Repository;

namespace API_OVH.Models.Manager
{
    /// <summary>
    /// Manager pour gérer les opérations liées aux Directions.
    /// </summary>
    public class DirectionManager : IReadOnlyDataRepository<Direction>
    {
        private readonly SAE5_BD_OVH_DbContext _context;
        private readonly IMapper _mapper;

        [ActivatorUtilitiesConstructor]
        public DirectionManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DirectionManager()
        {
        }

        /// <summary>
        /// Retourne la liste des Directions de façon asynchrone
        /// </summary>
        /// <returns>La liste des Directions</returns>
        public async Task<ActionResult<IEnumerable<Direction>>> GetAllAsync()
        {
            var Directions = await _context.Directions.ToListAsync();

            return Directions;
        }

        /// <summary>
        /// Retourne une direction selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du Direction</param>
        /// <returns>Le Direction correspondant à l'ID</returns>
        public async Task<ActionResult<Direction>> GetByIdAsync(int id)
        {
            var leDirection = await _context.Directions.FirstOrDefaultAsync(x => x.Iddirection == id);

            // S'il n'est pas trouvé
            if (leDirection == null)
            {
                return new NotFoundResult();
            }

            return leDirection;
        }

        /// <summary>
        /// Retourne une direction selon sa lettre (N, S, E, O, SE, SO, NO, NE)
        /// </summary>
        /// <param name="str">Lettre correspondant à la direction</param>
        /// <returns>Le Direction correspondant au nom spécifié</returns>
        public async Task<ActionResult<Direction>> GetByStringAsync(string str)
        {
            var leDirection = await _context.Directions.FirstOrDefaultAsync(x => x.Lettres_direction == str);

            // Si non trouvé
            if (leDirection == null)
            {
                return new NotFoundResult();
            }

            return leDirection;
        }
    }
}