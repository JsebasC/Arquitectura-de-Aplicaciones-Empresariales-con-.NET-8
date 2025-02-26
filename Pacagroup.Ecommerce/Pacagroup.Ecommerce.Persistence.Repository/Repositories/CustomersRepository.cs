using Dapper;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Persistence.Contexts;
using System.Data;

namespace Pacagroup.Ecommerce.Persistence.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DapperContext _dapperContext;

        public CustomersRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        #region Metodos Sincronos
        public bool Insert(Customer customer)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = connection!.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public bool Update(Customer customer)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = connection!.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public bool Delete(string customerId)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customerId);
                var result = connection!.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public Customer Get(string customerId)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customerId);
                return connection!.QuerySingle<Customer>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersList";
                return connection!.Query<Customer>(query, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Customer> GetAllWithPagination(int pageNumber, int pageSize)
        {

            using var connection = _dapperContext.CreateConnection();
            const string query = "CustomersListWithPagination";
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
            return connection!.Query<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);
        }
        public int Count()
        {
            using var connection = _dapperContext.CreateConnection();
            var query = "SELECT COUNT(*) from Customers";
            return connection.ExecuteScalar<int>(query, commandType: CommandType.Text);
        }


        #endregion

        #region Metodos Asincronos

        public async Task<bool> InsertAsync(Customer customer)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = await connection!.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customer.CustomerId);
                parameters.Add("CompanyName", customer.CompanyName);
                parameters.Add("ContactName", customer.ContactName);
                parameters.Add("ContactTitle", customer.ContactTitle);
                parameters.Add("Address", customer.Address);
                parameters.Add("City", customer.City);
                parameters.Add("Region", customer.Region);
                parameters.Add("PostalCode", customer.PostalCode);
                parameters.Add("Country", customer.Country);
                parameters.Add("Phone", customer.Phone);
                parameters.Add("Fax", customer.Fax);

                var result = await connection!.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customerId);
                var result = await connection!.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
 
        public async Task<Customer> GetAsync(string customerId)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerID", customerId);
                return await connection!.QuerySingleAsync<Customer>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                const string query = "CustomersList";
                return await connection!.QueryAsync<Customer>(query, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = "CustomersListWithPagination";
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);

            var customers = await connection.QueryAsync<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return customers;
        }

        public async Task<int> CountAsync()
        {
            using var connection = _dapperContext.CreateConnection();
            var query = "Select Count(*) from Customers";

            var count = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
            return count;
        }

        #endregion
    }
}
