using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface ISalleRepository<TEntity, TEntitySansNavigationDTO, TEntityDTO, TEntityDetailDTO>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByIdWithoutDTOAsync(int id);
        Task<ActionResult<TEntityDetailDTO>> GetByStringAsync(string str);
        Task AddAsync(TEntitySansNavigationDTO entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
