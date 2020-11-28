using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitas.Models;
using Visitas.Repositories.Interfaces;

namespace Visitas.Repositories.Dapper.Implementaciones
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString) { }

        public Users ValidateUser(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@password", password);

                return connection.QueryFirstOrDefault<Users>("dbo.UspValidateUser", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public Users CreateUser(Users user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                String message = "";
                var parameters = new DynamicParameters();
                parameters.Add("@email", user.Email);
                parameters.Add("@password", user.Password);
                parameters.Add("@firstName", user.FirstName);
                parameters.Add("@lastName", user.LastName);
                parameters.Add("@roles", user.Roles);
                parameters.Add("@ov_message_error", message);

                return connection.QueryFirstOrDefault<Users>("dbo.UspCreateUser", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
