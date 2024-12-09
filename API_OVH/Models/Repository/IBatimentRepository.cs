using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IBatimentRepository<TEntity, TEntityDTO, TEntitySansNavigationDTO>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string str);
        Task AddAsync(TEntitySansNavigationDTO entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
