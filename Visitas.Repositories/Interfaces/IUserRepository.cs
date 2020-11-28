using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitas.Models;

namespace Visitas.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<Users>
    {
        Users ValidateUser(string email, string password);
        Users CreateUser(Users user);
    }
}
