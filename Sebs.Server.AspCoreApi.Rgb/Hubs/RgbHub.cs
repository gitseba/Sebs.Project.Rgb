using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Sebs.Common.Rgb.Models;

namespace Sebs.Server.AspCoreApi.Rgb
{
    /// <summary>
    /// Central tower for Rgb hub to receive and send further messages from clients (Console, Wpf etc)
    /// </summary>
    [AllowAnonymous]
    public class RgbHub : Hub
    {
        public async Task PublishColorToAll(RgbModel color)
        {
            await Clients
                .All
                .SendAsync("RgbMessage", color);
        }
    }
}
