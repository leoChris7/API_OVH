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
    public class SalleManager : ISalleRepository<Salle, SalleSansNavigationDTO, SalleDTO, SalleDetailDTO>
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
        /// Retourne la liste de toutes les Salles sous forme de DTO
        /// </summary>
        /// <returns>La liste des SallesDTO</returns>
        public async Task<ActionResult<IEnumerable<SalleDTO>>> GetAllAsync()
        {
            return await dbContext.Salles
                .ProjectTo<SalleDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Retourne une SalleDetailDTO selon son id
        /// </summary>
        /// <param name="id">(Entier) Identifiant de la Salle</param>
        /// <returns>La SalleDetailDTO correspondante à l'ID</returns>
        public async Task<ActionResult<SalleDetailDTO>> GetByIdAsync(int id)
        {
            var result = await dbContext.Salles
                .Where(u => u.IdSalle == id)
                .Include(s => s.Murs) // Inclure les murs
                    .ThenInclude(m => m.Capteurs) // Inclure les capteurs pour chaque mur
                .Include(s => s.Murs)
                    .ThenInclude(m => m.Equipements) // Inclure les équipements pour chaque mur
                .ProjectTo<SalleDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (result == null)
                return new NotFoundResult();

            return result;
        }


        /// <summary>
        /// Retourne une Salle selon son id
        /// </summary>
        /// <param name="id">(Entier) Identifiant de la Salle</param>
        /// <returns>La salle correspondante à l'ID</returns>
        public async Task<ActionResult<Salle>> GetByIdWithoutDTOAsync(int id)
        {
            var salle = await dbContext.Salles
                .FirstOrDefaultAsync(t => t.IdSalle == id);

            if (salle == null) return new NotFoundResult();

            return salle;
        }

        /// <summary>
        /// Retourne une SalleDetailDTO selon son nom
        /// </summary>
        /// <param name="nom">Nom de la Salle</param>
        /// <returns>La SalleDetailDTO correspondante au nom spécifié</returns>
        public async Task<ActionResult<SalleDetailDTO>> GetByStringAsync(string nom)
        {
            var salle = await dbContext.Salles
                .Where(u => u.NomSalle.ToUpper() == nom.ToUpper())
                .ProjectTo<SalleDetailDTO>(mapper.ConfigurationProvider) // Mapper vers UniteDetailDTO
                .FirstOrDefaultAsync();

            if (salle == null)
                return new NotFoundResult();

            return new ActionResult<SalleDetailDTO>(salle);
        }

        /// <summary>
        /// Ajoute une Salle de façon asynchrone à partir d'un DTO sans navigation
        /// </summary>
        /// <param name="entity">SalleSansNavigation (DTO) à ajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(SalleSansNavigationDTO entity)
        {
            var salle = mapper.Map<Salle>(entity);

            await dbContext.Salles.AddAsync(salle);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour une Salle à partir de l'entité existante
        /// </summary>
        /// <param name="entityToUpdate">Salle existante</param>
        /// <param name="entity">Salle avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Salle entityToUpdate, Salle entity)
        {
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;

            entityToUpdate.IdBatiment = entity.IdBatiment;
            entityToUpdate.IdTypeSalle = entity.IdTypeSalle;
            entityToUpdate.NomSalle = entity.NomSalle;

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer une Salle de façon asynchrone
        /// </summary>
        /// <param name="entity">Salle à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(Salle entity)
        {
            dbContext.Salles.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
