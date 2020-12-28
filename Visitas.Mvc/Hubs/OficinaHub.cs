using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Visitas.Mvc.Hubs
{
    public class OficinaHub: Hub
    {
        static List<string> OficinaIds = new List<string>();
        public void AddOficinaId(string id)
        {
            if (!OficinaIds.Contains(id))
            {
                OficinaIds.Add(id);
            }
            Clients.All.oficinaStatus(OficinaIds);
        }
        public void RemoveOficinaId(string id)
        {
            if (OficinaIds.Contains(id)) OficinaIds.Remove(id);
            Clients.All.oficinaStatus(OficinaIds);
        }
        public override Task OnConnected()
        {
            return Clients.All.oficinaStatus(OficinaIds);
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