using API_OVH.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IUniteRepository
    {
        public interface IUniteRepository<TEntityDTO, TEntityDetailDTO>
        {
            Task<ActionResult<IEnumerable<TEntityDTO>>> GetAllAsync();
            Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
            Task<ActionResult<TEntityDTO>> GetByIdWithoutDTOAsync(int id);
            Task<ActionResult<TEntityDetailDTO>> GetByStringAsync(string nomUnite);
            Task<ActionResult<TEntityDetailDTO>> GetBySigleAsync(string nomSigle);
            Task AddAsync(TEntityDTO entity);
            Task UpdateAsync(TEntityDTO entityToUpdate, TEntityDTO entity);
            Task DeleteAsync(TEntityDTO entity);
        }
    }
}
