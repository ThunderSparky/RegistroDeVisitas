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
    public class OficinaRepository : Repository<Oficinas>, IOficinaRepository
    {
        public OficinaRepository(string connectionString) : base(connectionString)
        {

        }
        public IEnumerable<Oficinas> PagedList(int startRow, int endRow)
        {
            //Validamos que la fila de inicio sea mayor o igual que la fila final
            if (startRow >= endRow) return new List<Oficinas>(); //En caso ocurra que la fila de inicio sea menor a la del final, retornamos una lista vacia
            //En caso la fila de inicio sea MAYOR que la fila final prcedemos con lo siguiente:
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@startRow", startRow);
                parameters.Add("@endRow", endRow);
                return connection.Query<Oficinas>("dbo.uspOficinaPagedList",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int Count()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM dbo.Oficinas Where Estado = '1'");
            }
        }
    }
}
