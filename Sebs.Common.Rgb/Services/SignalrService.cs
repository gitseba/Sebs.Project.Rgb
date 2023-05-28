using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace Sebs.Common.Rgb.Services
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 12/19/2022 11:53:10 AM
    /// </summary>
    public class SignalrService : ISignalrService
    {
        private HubConnection _hubConnection;
        private string _hubUrl;
        private readonly int _counts;
        private readonly int _delaySeconds;
        private readonly ILogger? _logger;
        CancellationTokenSource cancellationToken;

        public SignalrService()
        {

        }

        public async Task<HubConnection> StartAsync(
            string hubUrl = "",
            int attemptsToReconnect = 0,
            int delaySeconds = 1,
            bool shouldAutoReconnect = true)
        {
            try
            {
                _hubUrl = hubUrl;
                CreateConnection();
                var openConnectionResult = await OpenConnection();
                if (openConnectionResult != null)
                {
                    if (shouldAutoReconnect)
                    {
                        _logger?.LogError("Automatically retry to reconnect!");
                        _ = await AttemptToReconnect(_counts);
                    }
                    else
                    {
                        throw new Exception("Initialization failed. A connection with the Hub could not be opened. Please restart application to retry.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            _hubConnection.Closed += ClosedHandler;
            _hubConnection.Reconnecting += ReconnectingHandler;
            _hubConnection.Reconnected += ReconnectedHandler;

            return _hubConnection;
        }

        private Task? ReconnectedHandler(string arg)
        {
            _logger?.LogInformation($"Reconnected. {arg}");
            cancellationToken.Cancel(false);
            return null;
        }

        private async Task ReconnectingHandler(Exception arg)
        {
            _logger?.LogInformation($"Reconnecting. {arg.Message}");
            await AttemptToReconnect(attempts: _counts);
        }

        private Task? ClosedHandler(Exception arg)
        {
            _logger?.LogError($"Connection with the Hub was been closed. {arg.Message}");
            return null;
        }

        private void CreateConnection()
        {
            _logger?.LogInformation("Create a connection with SignalR Hub.");
            
            try
            {
                _hubConnection = new HubConnectionBuilder()
                   .WithUrl(_hubUrl, options =>
                   {
                       options.AccessTokenProvider = () => Task.FromResult("Sebs");
                   })
                   .WithAutomaticReconnect()
                   .Build();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> OpenConnection()
        {
            return await _hubConnection
                         .StartAsync()
                         .ContinueWith(task =>
                         {
                             if (task.Exception != null)
                             {
                                 _logger?.LogError(task.Exception.Message);
                                 return task.Exception.Message;
                             }

                             _logger.LogInformation("Connection is opened.");
                             return String.Empty;
                         });
        }

        private async Task<string> AttemptToReconnect(int attempts = 0)
        {
            var count = 1;
            string exception;
            cancellationToken = new CancellationTokenSource();
            do
            {
                _logger?.LogWarning($"Attempt {count} to reconnect ...");
                exception = await OpenConnection();
                if (cancellationToken.IsCancellationRequested || count == attempts)
                {
                    _logger?.LogError($"Reconnection succeed.");
                    break;
                }

                await Task.Delay(_delaySeconds * 1000);
                if (exception == null)
                {
                    _logger?.LogError($"Attempt {count}: Succeeded.");
                }
                else
                {
                    _logger?.LogError($"Attempt {count}: {exception}");
                    count++;
                }
            } while (exception != null);

            return exception;
        }

        public async Task SendMessageToHub<T>(string methodName, T model)
        {
            await _hubConnection.InvokeAsync(methodName, model);
        }
    }
}
