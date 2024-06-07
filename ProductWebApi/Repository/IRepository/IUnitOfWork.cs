namespace ProductWebApi.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        Task SaveAsync();
    }
}
