using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_OVH.Models.EntityFramework;
using AutoMapper;
using API_OVH.Models.DTO;
using AutoMapper.QueryableExtensions;

namespace API_OVH.Models.Manager
{
    /// <summary>
    /// Manager pour gérer les opérations liées aux capteurs.
    /// </summary>
    public class CapteurManager : ICapteurRepository<Capteur, CapteurDTO, CapteurDetailDTO, CapteurSansNavigationDTO>
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
        public async Task<ActionResult<IEnumerable<CapteurDTO>>> GetAllAsync()
        {
            var capteurs = await dbContext.Capteurs
                .ProjectTo<CapteurDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

            return capteurs;
        }

        /// <summary>
        /// Retourne un DTO détaillé du capteur selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du capteur</param>
        /// <returns>Les détails du capteur correspondant à l'ID</returns>
        public async Task<ActionResult<CapteurDetailDTO>> GetByIdAsync(int id)
        {
            return await dbContext.Capteurs
                .Where(t => t.IdCapteur == id)
                .ProjectTo<CapteurDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retourne un Capteur selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du capteur</param>
        /// <returns>Le capteur correspondant à l'ID</returns>
        public async Task<ActionResult<Capteur>> GetByIdWithoutDTOAsync(int id)
        {
            return await dbContext.Capteurs
                .Include(t => t.MurNavigation)
                .Include(t => t.UnitesCapteur)
                .FirstOrDefaultAsync(x => x.IdCapteur == id);
        }

        /// <summary>
        /// Retourne un capteur selon son nom de façon asynchrone
        /// </summary>
        /// <param name="nom">Nom du capteur</param>
        /// <returns>Le capteur correspondant au nom spécifié</returns>
        public async Task<ActionResult<CapteurDetailDTO>> GetByStringAsync(string nom)
        {
            return await dbContext.Capteurs
                .Where(t => t.NomCapteur == nom)
                .ProjectTo<CapteurDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Ajoute un capteur de façon asynchrone
        /// </summary>
        /// <param name="entity">capteur à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(CapteurSansNavigationDTO entity)
        {
            var capteur = mapper.Map<Capteur>(entity);

            await dbContext.Capteurs.AddAsync(capteur);
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
            foreach(UniteCapteur liaison in entity.UnitesCapteur)
            {
                dbContext.UnitesCapteur.Remove(liaison);
                await dbContext.SaveChangesAsync();
            }

            dbContext.Capteurs.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}