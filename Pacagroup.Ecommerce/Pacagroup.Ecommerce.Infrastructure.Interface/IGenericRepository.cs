﻿namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    public interface IGenericRepository<T> where T: class
    {
        #region Metodos Sincronos

        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string entityId);
        T Get(string entityId);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithPagination(int pageNumber, int pageSize);
        int Count();

        #endregion

        #region Metodos Asincronos

        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string entityId);
        Task<T> GetAsync(string entityId);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();

        #endregion
    }
}
