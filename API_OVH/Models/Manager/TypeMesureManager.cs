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
    /// Manager pour gérer les opérations liées aux types de mesure
    /// </summary>
    public class TypeMesureManager : IDataRepository<TypeMesure>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public TypeMesureManager() { }
        public TypeMesureManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de tous les types de mesure de façon asynchrone
        /// </summary>
        /// <returns>La liste des types de mesure</returns>
        public async Task<ActionResult<IEnumerable<TypeMesure>>> GetAllAsync()
        {
            return await dbContext.TypesMesure.ToListAsync();
        }

        /// <summary>
        /// Retourne un type de mesure selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du type de mesure</param>
        /// <returns>Le type de mesure correspondant à l'ID</returns>
        public async Task<ActionResult<TypeMesure>> GetByIdAsync(int id)
        {
            return await dbContext.TypesMesure.FirstOrDefaultAsync(t => t.IdTypeMesure == id);
        }

        /// <summary>
        /// Retourne un type de mesure selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom du type de mesure</param>
        /// <returns>Le type de mesure correspondant au nom spécifié</returns>
        public async Task<ActionResult<TypeMesure>> GetByStringAsync(string nom)
        {
            return await dbContext.TypesMesure.FirstOrDefaultAsync(t => t.NomTypeMesure.ToUpper() == nom.ToUpper());
        }

        /// <summary>
        /// Ajoute un type de mesure de façon asynchrone
        /// </summary>
        /// <param name="entity">type de mesure à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(TypeMesure entity)
        {
            await dbContext.TypesMesure.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour un type de mesure de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">type de mesure à mettre à jour</param>
        /// <param name="entity">type de mesure avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(TypeMesure TypeMesure, TypeMesure entity)
        {
            dbContext.Entry(TypeMesure).State = EntityState.Modified;
            TypeMesure.IdTypeMesure = entity.IdTypeMesure;
            TypeMesure.NomTypeMesure = entity.NomTypeMesure;
            dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un type de mesure de façon asynchrone
        /// </summary>
        /// <param name="entity">Le type de mesure à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(TypeMesure TypeMesure)
        {
            dbContext.TypesMesure.Remove(TypeMesure);
            await dbContext.SaveChangesAsync();
        }
    }
}
