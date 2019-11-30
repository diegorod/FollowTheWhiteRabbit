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
            await Groups.AddToGroupAsync(Context.ConnectionId, "Matrix");
            await Clients.Client(Context.ConnectionId).SendAsync("UpdateChat", $"Operator: You are plugged in.");
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UpdateChat", $"Operator: A new user ({Context.ConnectionId}) plugged in.");
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Matrix");
            await Clients.All.SendAsync("UpdateChat", $"Operator: A user ({Context.ConnectionId}) has unplugged.");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
