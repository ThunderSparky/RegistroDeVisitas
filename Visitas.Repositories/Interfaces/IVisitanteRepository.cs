using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitas.Models;

namespace Visitas.Repositories.Interfaces
{
    public interface IVisitanteRepository : IRepository<Visitantes>
    {
        IEnumerable<Visitantes> PagedList(int startRow, int endRow);
        int Count();
    }
}
