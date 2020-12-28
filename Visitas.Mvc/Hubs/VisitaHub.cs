using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Visitas.Mvc.Hubs
{
    public class VisitaHub: Hub
    {
        static List<string> VisitaIds = new List<string>();
        public void AddVisitaId(string id)
        {
            if (!VisitaIds.Contains(id))
            {
                VisitaIds.Add(id);
            }
            Clients.All.visitaStatus(VisitaIds);
        }
        public void RemoveVisitaId(string id)
        {
            if (VisitaIds.Contains(id)) VisitaIds.Remove(id);
            Clients.All.visitaStatus(VisitaIds);
        }
        public override Task OnConnected()
        {
            return Clients.All.visitaStatus(VisitaIds);
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