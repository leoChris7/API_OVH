using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface ITypeEquipementRepository<TEntityDTO, TEntityDetailDTO>
    {
        Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
        Task<ActionResult<TEntityDetailDTO>> GetByStringAsync(string str);
        Task AddAsync(TEntityDTO entity);
        Task UpdateAsync(TEntityDTO entityToUpdate, TEntityDTO entity);
        Task DeleteAsync(TEntityDTO entity);
    }
}
