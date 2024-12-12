using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IDirectionRepository<TEntityDetailDTO, TEntitySansNavigationDTO>
    { 
        Task<ActionResult<IEnumerable<TEntitySansNavigationDTO>>> GetAllAsync();
        Task<ActionResult<TEntityDetailDTO>> GetByIdAsync(int id);
        Task<ActionResult<TEntityDetailDTO>> GetByDegreAsync(decimal deg);
    }
}
