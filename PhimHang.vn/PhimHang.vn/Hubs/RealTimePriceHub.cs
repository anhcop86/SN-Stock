using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PhimHang.Hubs
{
    public class RealTimePriceHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}