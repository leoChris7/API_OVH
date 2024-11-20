using Microsoft.AspNetCore.Mvc;

namespace API_OVH.Models.Repository
{
    public interface IReadOnlyDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<TEntity>> GetByStringAsync(string str);
    }
}
