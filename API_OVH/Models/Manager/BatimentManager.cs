using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionProduit_API.Models.Repository;
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
        private readonly SAE5_BD_OVH_DbContext _context;
        private readonly IMapper _mapper;

        public BatimentManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste des batiments de façon asynchrone
        /// </summary>
        /// <returns>La liste des batiments</returns>
        public async Task<ActionResult<IEnumerable<Batiment>>> GetAllAsync()
        {
            var batiments = await _context.Batiments
                            .ToListAsync();

            return batiments;
        }

        /// <summary>
        /// Retourne un batiment selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du batiment</param>
        /// <returns>Le batiment correspondant à l'ID</returns>
        public async Task<ActionResult<Batiment>> GetByIdAsync(int id)
        {
            var leBatiment = await _context.Batiments
                             .FirstOrDefaultAsync(x => x.Idbatiment == id);

            // S'il n'est pas trouvé
            if (leBatiment == null)
            {
                return new NotFoundResult();
            }

            return leBatiment;
        }

        /// <summary>
        /// Retourne un batiment selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom du batiment</param>
        /// <returns>Le batiment correspondant au nom spécifié</returns>
        public async Task<ActionResult<Batiment>> GetByStringAsync(string str)
        {
            var leBatiment = await _context.Batiments
                             .FirstOrDefaultAsync(x => x.Nombatiment == str);

            // Si non trouvé
            if (leBatiment == null)
            {
                return new NotFoundResult();
            }

            return leBatiment;
        }

        /// <summary>
        /// Ajoute un batiment de façon asynchrone
        /// </summary>
        /// <param name="entity">Batiment à rajouter</param>
        /// <returns>Le batiment créée</returns>
        public async Task AddAsync(Batiment entity)
        {
            await _context.Batiments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour un batiment de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">Batiment à mettre à jour</param>
        /// <param name="entity">Batiment avec les valeurs mis à jour</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Batiment entityToUpdate, Batiment entity)
        {
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.Nombatiment = entity.Nombatiment;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un batiment de façon asynchrone
        /// </summary>
        /// <param name="entity">Le batiment à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(Batiment entity)
        {
            _context.Batiments.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}