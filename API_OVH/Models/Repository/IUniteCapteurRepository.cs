using API_OVH.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IUniteCapteurRepository<TEntity, TEntitySansNavigationDTO>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int idCapt, int idUnite);
        Task AddAsync(TEntitySansNavigationDTO entity);
        Task DeleteAsync(TEntity entity);
    }
}
