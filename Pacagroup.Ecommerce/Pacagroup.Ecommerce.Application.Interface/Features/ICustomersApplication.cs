using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface.Features
{
    public interface ICustomersApplication
    {
        #region Metodos Sincronos
        Response<bool> Insert(CustomerDto customersDto);
        Response<bool> Update(CustomerDto customersDto);
        Response<bool> Delete(string customerId);
        Response<CustomerDto> Get(string customerId);
        Response<IEnumerable<CustomerDto>> GetAll();
        ResponsePagination<IEnumerable<CustomerDto>> GetAllWithPagination(int pageNumber, int pageSize);
        #endregion

        #region Metodos Asincronos

        Task<Response<bool>> InsertAsync(CustomerDto customersDto);
        Task<Response<bool>> UpdateAsync(CustomerDto customersDto);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomerDto>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDto>>> GetAllAsync();
        Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        #endregion

    }
}
