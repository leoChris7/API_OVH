using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;

namespace API_OVH.Models.DataManager
{
    /// <summary>
    /// Manager pour gérer les opérations liées aux unités
    /// </summary>
    public class UniteManager : IDataRepository<Unite>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public UniteManager() { }
        public UniteManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de toutes les unites de façon asynchrone
        /// </summary>
        /// <returns>La liste des unites</returns>
        public async Task<ActionResult<IEnumerable<Unite>>> GetAllAsync()
        {
            return await dbContext.Unites.ToListAsync();
        }

        /// <summary>
        /// Retourne une unite selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant de l'unite</param>
        /// <returns>L'unite correspondante à l'ID</returns>
        public async Task<ActionResult<Unite>> GetByIdAsync(int id)
        {
            return await dbContext.Unites.FirstOrDefaultAsync(t => t.IdUnite == id);
        }

        /// <summary>
        /// Retourne une unite selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom de l'unite</param>
        /// <returns>L'unite correspondante au nom spécifié</returns>
        public async Task<ActionResult<Unite>> GetByStringAsync(string nom)
        {
            return await dbContext.Unites.FirstOrDefaultAsync(t => t.NomUnite.ToUpper() == nom.ToUpper());
        }

        /// <summary>
        /// Ajoute une unite de façon asynchrone
        /// </summary>
        /// <param name="entity">Unite à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(Unite entity)
        {
            await dbContext.Unites.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour une unite de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">Unite à mettre à jour</param>
        /// <param name="entity">Unite avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Unite Unite, Unite entity)
        {
            dbContext.Entry(Unite).State = EntityState.Modified;
            Unite.IdUnite = entity.IdUnite;
            Unite.NomUnite = entity.NomUnite;
            Unite.SigleUnite = entity.SigleUnite;
            dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer une unite de façon asynchrone
        /// </summary>
        /// <param name="entity">L'unite à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(Unite Unite)
        {
            dbContext.Unites.Remove(Unite);
            await dbContext.SaveChangesAsync();
        }
    }
}
