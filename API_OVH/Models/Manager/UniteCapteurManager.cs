using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using API_OVH.Models.Repository;

namespace API_OVH.Models.DataManager
{
    public class UniteCapteurManager : IUniteCapteurRepository<UniteCapteur, UniteCapteurSansNavigationDTO>
    {
        readonly SAE5_BD_OVH_DbContext? dbContext;
        readonly IMapper mapper;

        public UniteCapteurManager() { }

        public UniteCapteurManager(SAE5_BD_OVH_DbContext context, IMapper mapper)
        {
            dbContext = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Retourne une liaison Unite Capteur à partir des deux ids
        /// </summary>
        public async Task<ActionResult<UniteCapteur>> GetByIdAsync(int idCapt, int idUnite)
        {
            return await dbContext.UnitesCapteur
                .Where(u => u.IdCapteur == idCapt && u.IdUnite == idUnite)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Ajoute une liaison entre un Capteur et une Unite
        /// </summary>
        public async Task AddAsync(UniteCapteurSansNavigationDTO entity)
        {
            var uniteCapteur = mapper.Map<UniteCapteur>(entity);

            await dbContext.UnitesCapteur.AddAsync(uniteCapteur);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime une unité
        /// </summary>
        public async Task DeleteAsync(UniteCapteur entity)
        {
            dbContext.UnitesCapteur.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}
