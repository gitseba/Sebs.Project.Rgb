using Microsoft.AspNetCore.SignalR.Client;
using Sebs.Client.Wpf.Rgb.Features.Commands;
using Sebs.Client.Wpf.Rgb.Models;
using Sebs.Common.Rgb.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Sebs.Client.Wpf.Rgb.Mvvm.ViewModel
{
    /// <summary>
    /// Purpose: 
    /// Created by: Sebastian
    /// Created at: 3/24/2021 10:57:24 AM
    /// </summary>
    public class RgbViewModel : BaseViewModel
    {
        private readonly ISignalrService _signalrService;
        public RgbViewModel(ISignalrService signalrService)
        {
            _signalrService = signalrService;
            _ = _signalrService.StartAsync("https://localhost:7022/rgb");
            Error = new ErrorModel();
            Status = new StatusModel();
            Rgb = new RgbModel();
            RgbMessages = new ObservableCollection<BrushViewModel>();

            SendColorCommand = new RelayCommand((p) => SendColorCommandHandler());
        }

        public ObservableCollection<BrushViewModel> RgbMessages { get; }
        public RgbModel Rgb { get; set; }
        public ErrorModel Error { get; set; }
        public StatusModel Status { get; set; }
        public ICommand SendColorCommand { get; set; }

        private async void SendColorCommandHandler()
        {
            try
            {
                var color = new RgbModel()
                {
                    Red = Rgb.Red,
                    Green = Rgb.Green,
                    Blue = Rgb.Blue,
                };

                await _signalrService.SendMessageToHub<RgbModel>("PublishColorToAll", color);

                Status.Message = "Message arrived at hub!"; 
                RgbMessages.Add(new BrushViewModel(color));
            }
            catch (Exception ex)
            {
                Error.Message = $"Unable to send color message. {ex.Message}";
            }
        }
    }
}
