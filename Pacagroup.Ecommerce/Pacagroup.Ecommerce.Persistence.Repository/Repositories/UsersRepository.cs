using Dapper;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Persistence.Contexts;

namespace Pacagroup.Ecommerce.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _dapperContext;

        public UsersRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public User Authenticate(string userName, string password)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);
                parameters.Add("Password", password);
                var user = connection.QuerySingle<User>(query, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                return user;
            }
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public bool Delete(string entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string entityId)
        {
            throw new NotImplementedException();
        }

        public User Get(string entityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllWithPagination(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(string entityId)
        {
            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
