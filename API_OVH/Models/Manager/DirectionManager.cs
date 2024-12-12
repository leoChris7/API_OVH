using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_OVH.Models.EntityFramework;
using AutoMapper;
using API_OVH.Models.Repository;
using API_OVH.Models.DTO;
using AutoMapper.QueryableExtensions;

namespace API_OVH.Models.Manager
{
    /// <summary>
    /// Manager pour gérer les opérations liées aux Directions.
    /// </summary>
    public class DirectionManager : IDirectionRepository<DirectionDetailDTO, DirectionSansNavigationDTO>
    {
        private readonly SAE5_BD_OVH_DbContext dbContext;
        private readonly IMapper mapper;

        [ActivatorUtilitiesConstructor]
        public DirectionManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        public DirectionManager()
        {
        }

        /// <summary>
        /// Retourne la liste de toutes les directions de façon asynchrone
        /// </summary>
        /// <returns>La liste des directions</returns>
        public async Task<ActionResult<IEnumerable<DirectionSansNavigationDTO>>> GetAllAsync()
        {
            var capteurs = await dbContext.Directions
                .ProjectTo<DirectionSansNavigationDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

            return capteurs;
        }

        /// <summary>
        /// Retourne une direction selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant de la Direction</param>
        /// <returns>La Direction correspondante à l'ID</returns>
        public async Task<ActionResult<DirectionDetailDTO>> GetByIdAsync(int id)
        {
            return await dbContext.Directions
                .ProjectTo<DirectionDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.IdDirection == id);
        }

        /// <summary>
        /// Retourne une direction selon un degre (la direction se rapprochant le plus du degre) de façon asynchrone
        /// </summary>
        /// <param name="deg">(Decimal) Degre recherche</param>
        /// <returns>La Direction correspondante au degre</returns>
        public async Task<ActionResult<DirectionDetailDTO>> GetByDegreAsync(decimal deg)
        {
            string[] dir = { "N", "NE", "E", "SE", "S", "SO", "O", "NO" };
            string dirCorresp = dir[(int)Math.Round(((deg % 360 + 360) % 360) / 45) % 8]; // deg % 360 + 360 : Normalisation de l'angle pour gérer les angles négatifs et / 45 chaque secteur

            return await dbContext.Directions
                .ProjectTo<DirectionDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.LettresDirection == dirCorresp);
        }
    }
}