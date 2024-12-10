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
    /// Manager pour gérer les opérations liées aux murs
    /// </summary>
    public class MurManager : IMurRepository<Mur, MurDTO, MurSansNavigationDTO>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public MurManager() { }
        public MurManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de tous les Murs de façon asynchrone
        /// </summary>
        /// <returns>La liste des Murs</returns>
        public async Task<ActionResult<IEnumerable<MurDTO>>> GetAllAsync()
        {
            return await dbContext.Murs
                .ProjectTo<MurDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Retourne un Mur selon son id de façon asynchrone
        /// </summary>
        /// <param name="id">(Entier) Identifiant du Mur</param>
        /// <returns>Le Mur correspondant à l'ID</returns>
        public async Task<ActionResult<Mur>> GetByIdAsync(int id)
        {
            return await dbContext.Murs.FirstOrDefaultAsync(t => t.IdMur == id);
        }

        /// <summary>
        /// Ajoute un Mur de façon asynchrone
        /// </summary>
        /// <param name="entity">Mur à rajouter</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task AddAsync(MurSansNavigationDTO entity)
        {
            var mur = mapper.Map<Mur>(entity);

            await dbContext.Murs.AddAsync(mur);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour un Mur de façon asynchrone
        /// </summary>
        /// <param name="entityToUpdate">Mur à mettre à jour</param>
        /// <param name="entity">Mur avec les nouvelles valeurs</param>
        /// <returns>Résultat de l'opération</returns>
        public async Task UpdateAsync(Mur Mur, Mur entity)
        {
            dbContext.Entry(Mur).State = EntityState.Modified;
            Mur.IdMur = entity.IdMur;
            Mur.IdDirection = entity.IdDirection;
            Mur.IdSalle = entity.IdSalle;
            Mur.Longueur = entity.Longueur;
            Mur.Hauteur = entity.Hauteur;
            Mur.Orientation = entity.Orientation;
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Permet de supprimer un Mur de façon asynchrone
        /// </summary>
        /// <param name="entity">Le Mur à supprimer</param>
        /// <returns>Le résultat de l'opération</returns>
        public async Task DeleteAsync(Mur Mur)
        {
            dbContext.Murs.Remove(Mur);
            await dbContext.SaveChangesAsync();
        }
    }
}
