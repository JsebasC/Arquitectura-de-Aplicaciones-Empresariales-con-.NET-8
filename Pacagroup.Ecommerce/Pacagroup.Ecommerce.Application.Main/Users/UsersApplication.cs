using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Features;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Features.Users
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UsersDtoValidator _validationRules;

        public UsersApplication(IUnitOfWork unitOfWork, IMapper mapper, UsersDtoValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public Response<UserDto> Authenticate(string userName, string password)
        {
            var response = new Response<UserDto>();
            var validacion = _validationRules.Validate(new UserDto() { UserName = userName, Password = password });
            if (!validacion.IsValid)
            {
                response.Message = "Errores de Validacion";
                response.Errors = validacion.Errors;
                return response;
            }
            try
            {
                var users = _unitOfWork.Users.Authenticate(userName, password);
                response.Data = _mapper.Map<UserDto>(users);
                response.IsSuccess = true;
                response.Message = "Autenticacion Exitosa!!!";
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Usuario no existe";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
