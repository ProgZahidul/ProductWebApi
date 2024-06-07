using ProductWebApi.Models;

namespace ProductWebApi.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task UpdateAsync(Category obj);
    }
}
