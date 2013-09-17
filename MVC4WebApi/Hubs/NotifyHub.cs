using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MVC4WebApi.Models;

namespace MVC4WebApi.Hubs
{
    public class NotifyHub : Hub
    {
        public void AddAccount(Account account)
        {
            Clients.All.AccountAdded();
        }
    }
}