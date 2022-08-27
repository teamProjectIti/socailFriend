using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SignalR
{
    public class presenceHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            base.OnConnectedAsync();

            await Clients.Others.SendAsync("UserIsOnline", Context.User.Identity.Name);
        }

    }
}
