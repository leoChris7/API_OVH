using API_OVH.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IUniteCapteurRepository<TEntity, TEntitySansNavigationDTO, TEntityDetailDTO>
    {
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int idCapt, int idUnite);
        Task<ActionResult<TEntity>> GetByIdWithoutDTOAsync(int idCapt, int idUnite);
        Task AddAsync(TEntitySansNavigationDTO entity);
        Task DeleteAsync(TEntity entity);
    }
}
