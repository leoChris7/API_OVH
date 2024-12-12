using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface ITypeSalleRepository<TEntity, TEntityDTO, TEntityDetailDTO>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByIdWithoutDTOAsync(int id);
        Task<ActionResult<TEntityDetailDTO>> GetByStringAsync(string str);
        Task AddAsync(TEntityDTO entity);
        Task UpdateAsync(TEntity entityToUpdate, TEntityDTO entity);
        Task DeleteAsync(TEntity entity);
    }
}
