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
    /// Manager pour gérer les opérations liées aux types d'equipement
    /// </summary>
    public class TypeEquipementManager : IDataRepository<TypeEquipement>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public TypeEquipementManager() { }
        public TypeEquipementManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de tous les types d'equipement de façon asynchrone
        /// </summary>
        /// <returns>La liste des types d'equipement</returns>
        public async Task<ActionResult<IEnumerable<TypeEquipement>>> GetAllAsync()
        {
            return await dbContext.TypesEquipement.ToListAsync();
        }

        /// <summary>
        /// Retourne un type d'equipement selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du type d'equipement</param>
        /// <returns>Le type d'equipement correspondant à l'ID</returns>
        public async Task<ActionResult<TypeEquipement>> GetByIdAsync(int id)
        {
            return await dbContext.TypesEquipement.FirstOrDefaultAsync(t => t.IdTypeEquipement == id);
        }

        /// <summary>
        /// Retourne un type d'equipement selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom du type d'equipement</param>
        /// <returns>Le type d'equipement correspondant au nom spécifié</returns>
        public async Task<ActionResult<TypeEquipement>> GetByStringAsync(string nom)
        {
            return await dbContext.TypesEquipement.FirstOrDefaultAsync(t => t.NomTypeEquipement.ToUpper() == nom.ToUpper());
        }

        /// <summary>
        /// Ajoute un type d'equipement de façon asynchrone
        /// </summary>
        /// <param name="entity">type d'equipement à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(TypeEquipement entity)
        {
            await dbContext.TypesEquipement.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour un type d'equipement de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">type d'equipement à mettre à jour</param>
        /// <param name="entity">type d'equipement avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(TypeEquipement TypeEquipement, TypeEquipement entity)
        {
            dbContext.Entry(TypeEquipement).State = EntityState.Modified;
            TypeEquipement.IdTypeEquipement = entity.IdTypeEquipement;
            TypeEquipement.NomTypeEquipement = entity.NomTypeEquipement;
            dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un type d'equipement de façon asynchrone
        /// </summary>
        /// <param name="entity">Le type d'equipement à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(TypeEquipement TypeEquipement)
        {
            dbContext.TypesEquipement.Remove(TypeEquipement);
            await dbContext.SaveChangesAsync();
        }
    }
}
