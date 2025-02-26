using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    public class UsersDomain : IUsersDomain
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Users Authenticate(string userName, string password)
        {
            return _unitOfWork.Users.Authenticate(userName, password);
        }
    }
}
