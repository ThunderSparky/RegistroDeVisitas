using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitas.Models;

namespace Visitas.Repositories.Interfaces
{
    public interface IInstitutoRepository : IRepository<Institutos>
    {
        IEnumerable<Institutos> PagedList(int startRow, int endRow);
        int Count();
        List<Institutos> GetByName(string name);
        List<Institutos> GetByNum(string name);
        List<Institutos> GetByNameAndNum(string name, string num);
    }
}
