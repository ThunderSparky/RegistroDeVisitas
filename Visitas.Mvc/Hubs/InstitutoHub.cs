using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Visitas.Mvc.Hubs
{
    public class InstitutoHub: Hub
    {
        static List<string> InstitutoIds = new List<string>();
        public void AddInstitutoId(string id)
        {
            if (!InstitutoIds.Contains(id))
            {
                InstitutoIds.Add(id);
            }
            Clients.All.institutoStatus(InstitutoIds);
        }
        public void RemoveInstitutoId(string id)
        {
            if (InstitutoIds.Contains(id)) InstitutoIds.Remove(id);
            Clients.All.institutoStatus(InstitutoIds);
        }
        public override Task OnConnected()
        {
            return Clients.All.institutoStatus(InstitutoIds);
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