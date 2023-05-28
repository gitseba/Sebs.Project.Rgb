using Microsoft.AspNetCore.SignalR.Client;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using Sebs.Client.Wpf.Rgb.Mvvm.ViewModel;
using Sebs.Client.Wpf.Rgb.Mvvm.Views;
using Sebs.Common.Rgb.Services;
using System.Windows;

namespace Sebs.Client.Wpf.Rgb
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 5/28/2023 11:12:10 PM
    /// </summary>
    public partial class App : PrismApplication
    {
        public HubConnection Connection { get; private set; }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(Connection);
            containerRegistry.Register<ISignalrService, SignalrService>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<ShellWindow, ShellViewModel>();

            ViewModelLocationProvider.Register<RgbView, RgbViewModel>();
        }
    }
}
