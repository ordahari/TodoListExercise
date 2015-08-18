using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRWithAuth.Hubs
{
    public class AuthorizeEcho : Hub
    {
        
        [Authorize]
        public string Echo(string toEcho)
        {
            return toEcho;
        }
    }
}
