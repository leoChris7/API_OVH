﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.Repository;
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
        private readonly SAE5_BD_OVH_DbContext dbContext;
        private readonly IMapper mapper;

        [ActivatorUtilitiesConstructor]
        public DirectionManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            mapper = mapper;
        }

        public DirectionManager()
        {
        }

        /// <summary>
        /// Retourne la liste de toutes les directions de façon asynchrone
        /// </summary>
        /// <returns>La liste des directions</returns>
        public async Task<ActionResult<IEnumerable<Direction>>> GetAllAsync()
        {
            return await dbContext.Directions.ToListAsync();
        }

        /// <summary>
        /// Retourne une direction selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant de la Direction</param>
        /// <returns>La Direction correspondante à l'ID</returns>
        public async Task<ActionResult<Direction>> GetByIdAsync(int id)
        {
            return await dbContext.Directions.FirstOrDefaultAsync(t => t.IdDirection == id);
        }

        /// <summary>
        /// Retourne une direction selon son diminutif (N, S, E, O, SE, SO, NO, NE)
        /// </summary>
        /// <param name="diminutif">diminutif correspondant à la direction</param>
        /// <returns>La Direction correspondante au nom spécifié</returns>
        public async Task<ActionResult<Direction>> GetByStringAsync(string diminutif)
        {
            return await dbContext.Directions.FirstOrDefaultAsync(t => t.LettresDirection.ToUpper() == diminutif.ToUpper());
        }

        /// <summary>
        /// Ajoute une direction de façon asynchrone
        /// </summary>
        /// <param name="entity">direction à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(Direction entity)
        {
            await dbContext.Directions.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour une direction de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">direction à mettre à jour</param>
        /// <param name="entity">direction avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Direction Direction, Direction entity)
        {
            dbContext.Entry(Direction).State = EntityState.Modified;
            Direction.IdDirection = entity.IdDirection;
            Direction.LettresDirection = entity.LettresDirection;
            dbContext.SaveChangesAsync();
        }
    }
}