using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface ISalleRepository<TEntitySansNavigationDTO, TEntityDTO, TEntityDetailDTO>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
        Task<ActionResult<TEntitySansNavigationDTO>> GetByIdWithoutDTOAsync(int id);
        Task<ActionResult<TEntityDetailDTO>> GetByStringAsync(string str);
        Task AddAsync(TEntitySansNavigationDTO entity);
        Task UpdateAsync(TEntitySansNavigationDTO entityToUpdate, TEntitySansNavigationDTO entity);
        Task DeleteAsync(TEntitySansNavigationDTO entity);
    }
}
