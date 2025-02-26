namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    public interface IGenericRepository<T> where T: class
    {
        #region Metodos Sincronos

        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string entityId);
        T Get(string entityId);
        IEnumerable<T> GetAll();

        #endregion

        #region Metodos Asincronos

        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string entityId);
        Task<T> GetAsync(string entityId);
        Task<IEnumerable<T>> GetAllAsync();

        #endregion
    }
}
