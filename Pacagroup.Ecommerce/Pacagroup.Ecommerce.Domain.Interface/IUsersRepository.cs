﻿using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Domain.Interface
{
    public interface IUsersRepository
    {
        Users Authenticate(string userName, string password);
    }
}
