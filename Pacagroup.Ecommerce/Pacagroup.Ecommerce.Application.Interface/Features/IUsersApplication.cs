using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface.Features
{
    public interface IUsersApplication
    {
        Response<UserDto> Authenticate(string userName, string password);
    }
}
