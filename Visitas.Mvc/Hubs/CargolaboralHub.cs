using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Visitas.Mvc.Hubs
{
    public class CargolaboralHub: Hub 
    {
        static List<string> CargolaboralIds = new List<string>();
        public void AddCargolaboralId(string id)
        {
            if (!CargolaboralIds.Contains(id))
            {
                CargolaboralIds.Add(id);
            }
            Clients.All.cargolaboralStatus(CargolaboralIds);
        } 
        public void RemoveCargolaboralId(string id)
        {
            if (CargolaboralIds.Contains(id)) CargolaboralIds.Remove(id);
            Clients.All.cargolaboralStatus(CargolaboralIds);
        }
        public override Task OnConnected()
        {
            return Clients.All.cargolaboralStatus(CargolaboralIds);
        }
        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
        public void AlertUpdate()
        {
            Clients.All.updateGrid();
        }
    }
}