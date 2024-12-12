using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IEquipementRepository<TEntityDTO, TEntityDetailDTO, TEntitySansNavigationDTO>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
        Task<ActionResult<TEntitySansNavigationDTO>> GetByIdWithoutDTOAsync(int id);
        Task<ActionResult<TEntityDetailDTO>> GetByStringAsync(string nomEquipement);
        Task AddAsync(TEntitySansNavigationDTO entity);
        Task UpdateAsync(TEntitySansNavigationDTO entityToUpdate, TEntitySansNavigationDTO entity);
        Task DeleteAsync(TEntitySansNavigationDTO entity);
    }
}
