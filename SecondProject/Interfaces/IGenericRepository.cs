namespace SecondProject.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(object id);
        Task<T> AddEntity(T entity);
        Task<T> UpdateEntity(T entity, object id);
        Task DeleteEntity(object id);

    }
}
