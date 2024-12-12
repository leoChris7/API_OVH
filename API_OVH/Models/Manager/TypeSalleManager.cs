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
    /// Manager pour gérer les opérations liées aux types de salle
    /// </summary>
    public class TypeSalleManager : ITypeSalleRepository<TypeSalle, TypeSalleDTO, TypeSalleDetailDTO>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public TypeSalleManager() { }
        public TypeSalleManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de tous les types de salle de façon asynchrone
        /// </summary>
        /// <returns>La liste des types de salle</returns>
        public async Task<ActionResult<IEnumerable<TypeSalleDTO>>> GetAllAsync()
        {
            // Récupère tous les TypeEquipement de la base
            var typesSalle = await dbContext.TypesSalle
                .ProjectTo<TypeSalleDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

            // Retourne la liste des DTO
            return typesSalle;
        }

        /// <summary>
        /// Retourne un type de salle selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du type de salle</param>
        /// <returns>Le type de salle correspondant à l'ID</returns>
        public async Task<ActionResult<TypeSalleDetailDTO>> GetByIdAsync(int id)
        {
            return await dbContext.TypesSalle
                .Include(t=>t.Salles)
                .ProjectTo<TypeSalleDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.IdTypeSalle == id);
        }


        /// <summary>
        /// Retourne un type de salle selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du type de salle</param>
        /// <returns>Le type de salle correspondant à l'ID</returns>
        public async Task<ActionResult<TypeSalle>> GetByIdWithoutDTOAsync(int id)
        {
            return await dbContext.TypesSalle
                .Include(t => t.Salles)
                .FirstOrDefaultAsync(t => t.IdTypeSalle == id);
        }

        /// <summary>
        /// Retourne un type de salle selon son nom de façon asynchrone
        /// </summary>
        /// <param name="str">Nom du type de salle</param>
        /// <returns>Le type de salle correspondant au nom spécifié</returns>
        public async Task<ActionResult<TypeSalleDetailDTO>> GetByStringAsync(string nom)
        {
            return await dbContext.TypesSalle
                .Include (t=>t.Salles)
                .ProjectTo<TypeSalleDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(t => t.NomTypeSalle.ToUpper() == nom.ToUpper());
        }

        /// <summary>
        /// Ajoute un type de salle de façon asynchrone
        /// </summary>
        /// <param name="entity">type de salle à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(TypeSalleDTO entity)
        {
            var typeSalle = mapper.Map<TypeSalle>(entity);

            await dbContext.TypesSalle.AddAsync(typeSalle);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour un type de salle de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">type de salle à mettre à jour</param>
        /// <param name="entity">type de salle avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(TypeSalle TypeSalle, TypeSalleDTO entity)
        {
            dbContext.Entry(TypeSalle).State = EntityState.Modified;

            TypeSalle.IdTypeSalle = entity.IdTypeSalle;
            TypeSalle.NomTypeSalle = entity.NomTypeSalle;

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un type de salle de façon asynchrone
        /// </summary>
        /// <param name="entity">Le type de salle à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(TypeSalle TypeSalle)
        {
            dbContext.TypesSalle.Remove(TypeSalle);
            await dbContext.SaveChangesAsync();
        }
    }
}
