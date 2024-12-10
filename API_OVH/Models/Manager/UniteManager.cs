using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;
using static API_OVH.Models.Repository.IUniteRepository;

namespace API_OVH.Models.DataManager
{
    public class UniteManager : IUniteRepository<Unite, UniteDTO, UniteDetailDTO>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public UniteManager() { }

        public UniteManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retourne la liste de toutes les unités sous forme de UniteDTO
        /// </summary>
        public async Task<ActionResult<IEnumerable<UniteDTO>>> GetAllAsync()
        {
            var result = await dbContext.Unites
                .ProjectTo<UniteDTO>(mapper.ConfigurationProvider) // Mapper les entités vers UniteDTO
                .ToListAsync();

            return new ActionResult<IEnumerable<UniteDTO>>(result);
        }

        /// <summary>
        /// Retourne une unité par son ID sous forme de UniteDetailDTO
        /// </summary>
        public async Task<ActionResult<UniteDetailDTO>> GetByIdAsync(int id)
        {
            var result = await dbContext.Unites
                .Where(u => u.IdUnite == id)
                .ProjectTo<UniteDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return new ActionResult<UniteDetailDTO>(result);
        }

        /// <summary>
        /// Retourne une unité par son ID sous forme de Unite
        /// </summary>
        public async Task<ActionResult<Unite>> GetByIdWithoutDTOAsync(int id)
        {
            var result = await dbContext.Unites
                .FirstOrDefaultAsync( u => u.IdUnite == id);

            return new ActionResult<Unite>(result);
        }

        /// <summary>
        /// Retourne une unité par son nom sous forme de UniteDetailDTO
        /// </summary>
        public async Task<ActionResult<UniteDetailDTO>> GetByStringAsync(string nom)
        {
            var result = await dbContext.Unites
                .Where(u => u.NomUnite.ToUpper() == nom.ToUpper())
                .ProjectTo<UniteDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return new ActionResult<UniteDetailDTO>(result);
        }

        /// <summary>
        /// Retourne une unité par son sigle sous forme de UniteDetailDTO
        /// </summary>
        public async Task<ActionResult<UniteDetailDTO>> GetBySigleAsync(string sigle)
        {
            var result = await dbContext.Unites
                .Where(u => u.SigleUnite.ToUpper() == sigle.ToUpper())
                .ProjectTo<UniteDetailDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return new ActionResult<UniteDetailDTO>(result);
        }

        /// <summary>
        /// Ajoute une unité à partir d'un UniteDTO
        /// </summary>
        public async Task AddAsync(UniteDTO dto)
        {
            // Mapper le DTO vers l'entité Unite
            var entity = mapper.Map<Unite>(dto);

            await dbContext.Unites.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Met à jour une unité existante
        /// </summary>
        public async Task UpdateAsync(Unite unite, Unite entity)
        {
            dbContext.Entry(unite).State = EntityState.Modified;

            unite.IdUnite = entity.IdUnite;
            unite.NomUnite = entity.NomUnite;
            unite.SigleUnite = entity.SigleUnite;

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime une unité
        /// </summary>
        public async Task DeleteAsync(Unite unite)
        {
            dbContext.Unites.Remove(unite);
            await dbContext.SaveChangesAsync();
        }
    }
}
