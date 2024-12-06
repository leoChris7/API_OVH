﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_OVH.Models.EntityFramework;
using AutoMapper;

namespace API_OVH.Models.Manager
{
    /// <summary>
    /// Manager pour gérer les opérations liées aux capteurs.
    /// </summary>
    public class CapteurManager : IDataRepository<Capteur>
    {
        private readonly SAE5_BD_OVH_DbContext dbContext;
        private readonly IMapper mapper;

        [ActivatorUtilitiesConstructor]
        public CapteurManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        public CapteurManager()
        {
        }

        /// <summary>
        /// Retourne la liste de tous les capteurs de façon asynchrone
        /// </summary>
        /// <returns>La liste des capteurs</returns>
        public async Task<ActionResult<IEnumerable<Capteur>>> GetAllAsync()
        {
            var capteurs = await dbContext.Capteurs.ToListAsync();

            return capteurs;
        }

        /// <summary>
        /// Retourne un capteur selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du capteur</param>
        /// <returns>Le capteur correspondant à l'ID</returns>
        public async Task<ActionResult<Capteur>> GetByIdAsync(int id)
        {
            var leCapteur = await dbContext.Capteurs.FirstOrDefaultAsync(x => x.IdCapteur == id);

            // S'il n'est pas trouvé
            if (leCapteur == null)
            {
                return new NotFoundResult();
            }

            return leCapteur;
        }

        /// <summary>
        /// Retourne un capteur selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom du capteur</param>
        /// <returns>Le capteur correspondant au nom spécifié</returns>
        public async Task<ActionResult<Capteur>> GetByStringAsync(string str)
        {
            var leCapteur = await dbContext.Capteurs.FirstOrDefaultAsync(x => x.NomCapteur == str);

            // Si non trouvé
            if (leCapteur == null)
            {
                return new NotFoundResult();
            }

            return leCapteur;
        }

        /// <summary>
        /// Ajoute un capteur de façon asynchrone
        /// </summary>
        /// <param name="entity">capteur à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(Capteur entity)
        {
            await dbContext.Capteurs.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour un capteur de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">capteur à mettre à jour</param>
        /// <param name="entity">capteur avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Capteur entityToUpdate, Capteur entity)
        {
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.NomCapteur = entity.NomCapteur;
            entityToUpdate.XCapteur = entity.XCapteur;
            entityToUpdate.YCapteur = entity.YCapteur;
            entityToUpdate.ZCapteur = entity.ZCapteur;
            entityToUpdate.EstActif = entity.EstActif;
            entityToUpdate.IdMur = entity.IdMur;
            entityToUpdate.IdCapteur = entity.IdCapteur;

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un capteur de façon asynchrone
        /// </summary>
        /// <param name="entity">Le capteur à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(Capteur entity)
        {
            dbContext.Capteurs.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}