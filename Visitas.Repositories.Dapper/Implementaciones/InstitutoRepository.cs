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
    public class InstitutoRepository : Repository<Institutos>, IInstitutoRepository
    {
        public InstitutoRepository(string connectionString) : base(connectionString)
        {

        }
        public IEnumerable<Institutos> PagedList(int startRow, int endRow)
        {
            //Validamos que la fila de inicio sea mayor o igual que la fila final
            if (startRow >= endRow) return new List<Institutos>(); //En caso ocurra que la fila de inicio sea menor a la del final, retornamos una lista vacia
            //En caso la fila de inicio sea MAYOR que la fila final prcedemos con lo siguiente:
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);
                return connection.Query<Institutos>("dbo.uspInstitutoPagedList",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM dbo.Institutos Where Estado = '1'");
            }
        }
        public List<Institutos> GetByName(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@name", name);
                return (List<Institutos>)connection.Query<Institutos>("dbo.uspGetInstitutosByName",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public List<Institutos> GetByNum(string num)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@num", num);
                return (List<Institutos>)connection.Query<Institutos>("dbo.uspGetInstitutosByNum",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public List<Institutos> GetByNameAndNum(string name, string num)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@name", name);
                parameters.Add("@num", num);
                return (List<Institutos>)connection.Query<Institutos>("dbo.uspGetInstitutosByNameAndNum",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
