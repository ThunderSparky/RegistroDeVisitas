using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitas.Repositories.Interfaces;
using Visitas.UnitOfWork;

namespace Visitas.Repositories.Dapper.Implementaciones
{
    public class VisitasUnitOfWork : IUnitOfWork
    {
        public VisitasUnitOfWork(string connectionString)
        {
            CargosLaborales = new CargosLaboralRepository(connectionString);
            Institutos = new InstitutoRepository(connectionString);
            Oficinas = new OficinaRepository(connectionString);
            Trabajadores = new TrabajadorRepository(connectionString);
            Visitantes = new VisitanteRepository(connectionString);
            Visitass = new VisitaRepository(connectionString);
            Users = new UserRepository(connectionString);
        }
        public ICargosLaboralRepository CargosLaborales
        {
            get;
            private set;
        }
        public IInstitutoRepository Institutos
        {
            get;
            private set;
        }
        public IOficinaRepository Oficinas
        {
            get;
            private set;
        }
        public ITrabajadorRepository Trabajadores
        {
            get;
            private set;
        }
        public IVisitanteRepository Visitantes
        {
            get;
            private set;
        }
        public IVisitaRepository Visitass
        {
            get;
            private set;
        }
        public IUserRepository Users
        {
            get;
            private set;
        }
    }
}
