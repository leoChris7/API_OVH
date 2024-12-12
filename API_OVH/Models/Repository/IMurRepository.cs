using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IMurRepository<TEntity, TEntityDTO, TEntityDetailDTO, TEntitySansNavigationDTO>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByIdWithoutDTOAsync(int id);
        Task AddAsync(TEntitySansNavigationDTO entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntitySansNavigationDTO entity);
        Task DeleteAsync(TEntity entity);
    }

}
