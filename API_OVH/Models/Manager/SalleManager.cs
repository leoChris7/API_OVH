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
    /// Manager pour gérer les opérations liées aux salles
    /// </summary>
    public class SalleManager : IDataRepository<Salle>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public SalleManager() { }
        public SalleManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de toutes les Salles de façon asynchrone
        /// </summary>
        /// <returns>La liste des Salles</returns>
        public async Task<ActionResult<IEnumerable<Salle>>> GetAllAsync()
        {
            return await dbContext.Salles.ToListAsync();
        }

        /// <summary>
        /// Retourne une Salle selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant de la Salle</param>
        /// <returns>La Salle correspondante à l'ID</returns>
        public async Task<ActionResult<Salle>> GetByIdAsync(int id)
        {
            return await dbContext.Salles.FirstOrDefaultAsync(t => t.IdSalle == id);
        }

        /// <summary>
        /// Retourne une Salle selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom de la Salle</param>
        /// <returns>La Salle correspondante au nom spécifié</returns>
        public async Task<ActionResult<Salle>> GetByStringAsync(string nom)
        {
            return await dbContext.Salles.FirstOrDefaultAsync(t => t.NomSalle.ToUpper() == nom.ToUpper());
        }

        /// <summary>
        /// Ajoute une Salle de façon asynchrone
        /// </summary>
        /// <param name="entity">Salle à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(Salle entity)
        {
            await dbContext.Salles.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour une Salle de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">Salle à mettre à jour</param>
        /// <param name="entity">Salle avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Salle Salle, Salle entity)
        {
            dbContext.Entry(Salle).State = EntityState.Modified;
            Salle.IdSalle = entity.IdSalle;
            Salle.IdBatiment = entity.IdBatiment;
            Salle.IdTypeSalle = entity.IdTypeSalle;
            Salle.NomSalle = entity.NomSalle;
            dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer une Salle de façon asynchrone
        /// </summary>
        /// <param name="entity">Salle à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(Salle Salle)
        {
            dbContext.Salles.Remove(Salle);
            await dbContext.SaveChangesAsync();
        }
    }
}
