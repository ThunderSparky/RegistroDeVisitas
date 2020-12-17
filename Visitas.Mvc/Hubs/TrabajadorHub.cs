using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Visitas.Mvc.Hubs
{
    public class TrabajadorHub: Hub
    {
        static List<string> TrabajadorIds = new List<string>();
        public void AddTrabajadorId(string id)
        {
            if (!TrabajadorIds.Contains(id))
            {
                TrabajadorIds.Add(id);
            }
            Clients.All.trabajadorStatus(TrabajadorIds);
        }
        public void RemoveTrabajadorId(string id)
        {
            if (TrabajadorIds.Contains(id)) TrabajadorIds.Remove(id);
            Clients.All.trabajadorStatus(TrabajadorIds);
        }
        public override Task OnConnected()
        {
            return Clients.All.trabajadorStatus(TrabajadorIds);
        }
        public void Message(string message)
        {
            Clients.All.getMessage(message);
        }
    }
}