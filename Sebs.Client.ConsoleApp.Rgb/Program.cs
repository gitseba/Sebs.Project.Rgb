using Microsoft.AspNetCore.SignalR.Client;
using Sebs.Common.Rgb.Models;

namespace Sebs.Client.ConsoleApp.Rgb
{
    public class Program
    {
        public static HubConnection Connection { get; private set; }

        static async Task Main(string[] args)
        {
            //Make proxy to hub based on hub name on server
            System.Console.WriteLine("Create connection with server hub...");
            //Set connection
            var connection = new HubConnectionBuilder()
                  .WithUrl("https://localhost:7022/rgb")
                  .WithAutomaticReconnect()
                  .Build();

            System.Console.WriteLine("\nStart connection with server....");
            await connection
                .StartAsync()
                .ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        System.Console.WriteLine("\nUnable to connect to server hub");
                    }
                });

            System.Console.WriteLine("\nClient subscribed to server message event...");
            connection
                .On("RgbMessage", (RgbModel color) =>
                {
                    System.Console.WriteLine($"\nReceived color: Red: {color.Red} / Blue: {color.Blue} / Green: {color.Green}");
                });

            #region Send Event
            //System.Console.WriteLine("\nClient sends message to server...");
            //await connection
            //    .SendAsync("PublishColorToAll", new RgbModel 
            //    {
            //        Blue = 15,
            //        Green = 25,
            //        Red = 152
            //    })
            //    .ContinueWith(task =>
            //    {
            //        if (task.IsFaulted)
            //        {
            //            System.Console.WriteLine(
            //                "\nThere was an error calling send: {0}",
            //                task.Exception.GetBaseException());
            //        }
            //        else
            //        {
            //            System.Console.WriteLine("\nMessage was send successfully.");
            //        }
            //    });

            ////Alternative method to SendAsync()
            //await connection.InvokeAsync("PublishStringColorToAll", "Red"); 
            #endregion

            var command = System.Console.ReadLine();
            if (command == "send")
            {
                await connection.SendAsync("PublishColorToAll", new RgbModel
                {
                    Blue = 155,
                    Green = 155,
                    Red = 155
                });
            }
            System.Console.ReadLine();
            await connection.StopAsync();
        }
    }
}
