using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Visitas.Mvc.Hubs
{
    public class VisitanteHub: Hub
    {
        static List<string> VisitanteIds = new List<string>();
        public void AddVisitanteId(string id)
        {
            if (!VisitanteIds.Contains(id))
            {
                VisitanteIds.Add(id);
            }
            Clients.All.visitanteStatus(VisitanteIds);
        }
        public void RemoveVisitanteId(string id)
        {
            if (VisitanteIds.Contains(id)) VisitanteIds.Remove(id);
            Clients.All.visitanteStatus(VisitanteIds);
        }
        public override Task OnConnected()
        {
            return Clients.All.visitanteStatus(VisitanteIds);
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