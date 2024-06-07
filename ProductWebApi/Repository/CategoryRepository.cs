using ProductWebApi.Dal_Models;
using ProductWebApi.Models;
using ProductWebApi.Repository.IRepository;

namespace ProductWebApi.Repository
{
   

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Category obj)
        {
            _db.categories.Update(obj);
            await Task.CompletedTask;
        }
    }
}
