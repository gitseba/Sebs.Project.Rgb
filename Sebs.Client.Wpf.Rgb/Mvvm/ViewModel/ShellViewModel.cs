using Sebs.Client.Wpf.Rgb.Features.Commands;
using System.Windows;
using System.Windows.Input;

namespace Sebs.Client.Wpf.Rgb.Mvvm.ViewModel
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 5/28/2023 11:12:10 PM
    /// </summary>
    public class ShellViewModel : BaseViewModel
    {
        public ShellViewModel()
        {
            // Window commands
            MinimizeCommand = new RelayCommand((p) => { WindowState = WindowState.Minimized; });
            MaximizeCommand = new RelayCommand((p) => { WindowState = WindowState.Maximized; });
            CloseCommand = new RelayCommand((p) => (p as Window)?.Close());
        }

        #region Binding properties
        public WindowState WindowState { get; set; } = WindowState.Normal;
        #endregion

        #region ICommands
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        #endregion
    }
}
