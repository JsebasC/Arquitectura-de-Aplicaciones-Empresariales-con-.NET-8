namespace Pacagroup.Ecommerce.Application.Interface.Presentation
{
    public interface  ICurrentUser
    {
        public string? UserId { get; }
        public string? UserName { get; }
    }
}
