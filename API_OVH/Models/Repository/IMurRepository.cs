using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IMurRepository<TEntityDTO, TEntityDetailDTO, TEntitySansNavigationDTO>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
        Task AddAsync(TEntitySansNavigationDTO entity);
        Task UpdateAsync(TEntitySansNavigationDTO entityToUpdate, TEntitySansNavigationDTO entity);
        Task DeleteAsync(TEntitySansNavigationDTO entity);
    }

}
