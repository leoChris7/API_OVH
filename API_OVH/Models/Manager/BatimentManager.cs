using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_OVH.Models.EntityFramework;
using AutoMapper;

namespace API_OVH.Models.Manager
{
    /// <summary>
    /// Manager pour gérer les opérations liées aux batiments
    /// </summary>
    public class BatimentManager : IDataRepository<Batiment>
    {
        private readonly SAE5_BD_OVH_DbContext dbContext;
        private readonly IMapper mapper;

        public BatimentManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de tous les batiments de façon asynchrone
        /// </summary>
        /// <returns>La liste de tous les batiments</returns>
        public async Task<ActionResult<IEnumerable<Batiment>>> GetAllAsync()
        {
            return await dbContext.Batiments.ToListAsync();
        }

        /// <summary>
        /// Retourne un batiment selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du batiment</param>
        /// <returns>Le batiment correspondant à l'ID</returns>
        public async Task<ActionResult<Batiment>> GetByIdAsync(int id)
        {
            return await dbContext.Batiments.FirstOrDefaultAsync(x => x.IdBatiment == id);
        }

        /// <summary>
        /// Retourne un batiment selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom du batiment</param>
        /// <returns>Le batiment correspondant au nom spécifié</returns>
        public async Task<ActionResult<Batiment>> GetByStringAsync(string str)
        {
            return await dbContext.Batiments.FirstOrDefaultAsync(x => x.NomBatiment == str);
        }

        /// <summary>
        /// Ajoute un batiment de façon asynchrone
        /// </summary>
        /// <param name="entity">Batiment à rajouter</param>
        /// <returns>Le resultat de la création</returns>
        public async Task AddAsync(Batiment entity)
        {
            await dbContext.Batiments.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour un batiment de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">Batiment à mettre à jour</param>
        /// <param name="entity">Batiment avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Batiment entityToUpdate, Batiment entity)
        {
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.NomBatiment = entity.NomBatiment;

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un batiment de façon asynchrone
        /// </summary>
        /// <param name="entity">Le batiment à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(Batiment entity)
        {
            dbContext.Batiments.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}