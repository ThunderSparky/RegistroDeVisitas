using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitas.Repositories.Interfaces;

namespace Visitas.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICargosLaboralRepository CargosLaborales { get; }
        IInstitutoRepository Institutos { get; }
        IOficinaRepository Oficinas { get; }
        ITrabajadorRepository Trabajadores { get; }
        IVisitanteRepository Visitantes { get; }
        IVisitaRepository Visitass { get; }
        IUserRepository Users { get; }
    }
}
