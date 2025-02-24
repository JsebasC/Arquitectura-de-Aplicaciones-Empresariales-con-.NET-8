using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Application.Validator;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUsersDomain _usersDomain;
        private readonly IMapper _mapper;
        private readonly UsersDtoValidator _validationRules;

        public UsersApplication(IUsersDomain usersDomain, IMapper mapper, UsersDtoValidator validationRules)
        {
            _usersDomain = usersDomain;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public Response<UsersDto> Authenticate(string userName, string password)
        {
            var response = new Response<UsersDto>();
            var validacion = _validationRules.Validate(new UsersDto() { UserName = userName, Password = password });
            if (!validacion.IsValid)
            {
                response.Message = "Errores de Validacion";
                response.Errors = validacion.Errors;
                return response;
            }
            try
            {
                var users = _usersDomain.Authenticate(userName, password);
                response.Data = _mapper.Map<UsersDto>(users);
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
