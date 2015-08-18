using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChetApp.Hubs
{
    public class ChatHub : Hub
    {

        public async Task Message(string message)
        {
           await Clients.All.message(message);
        }
    }
}
