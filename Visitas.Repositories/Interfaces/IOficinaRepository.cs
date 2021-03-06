﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitas.Models;

namespace Visitas.Repositories.Interfaces
{
    public interface IOficinaRepository : IRepository<Oficinas>
    {
        IEnumerable<Oficinas> PagedList(int startRow, int endRow);
        int Count();
    }
}
