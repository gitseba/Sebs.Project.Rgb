using Microsoft.AspNetCore.SignalR.Client;

namespace Sebs.Common.Rgb.Services
{
    public interface ISignalrService
    {
        Task<HubConnection> StartAsync(
            string hubUrl = "",
            int attemptsToReconnect = 0,
            int delaySeconds = 1,
            bool shouldAutoReconnect = true);

        Task SendMessageToHub<T>(string methodName, T model);
    }
}