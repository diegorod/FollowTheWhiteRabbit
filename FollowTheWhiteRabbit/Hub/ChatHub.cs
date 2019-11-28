using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FollowTheWhiteRabbit
{
    public class ChatHub : Hub
    {
        public async Task SubmitChat(string message)
        {
            await Clients.All.SendAsync("UpdateChat", message);
        }

        public async override Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("UpdateChat", $"Operator: You are plugged in.");
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UpdateChat", $"Operator: A new user plugged in.");
            await base.OnConnectedAsync();
        }
    }
}
