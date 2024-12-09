using API_OVH.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IUniteCapteurRepository<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdAsync(int idCapt, int idUnite);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
