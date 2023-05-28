
using Sebs.Client.Wpf.Rgb.Mvvm.ViewModel;

namespace Sebs.Client.Wpf.Rgb.Models
{
    /// <summary>
    /// Purpose: 
    /// Created by: Sebastian
    /// Created at: 3/24/2021 10:58:44 AM
    /// </summary>
    public class ErrorModel : BaseModel
    {
        private string _message = string.Empty;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
                OnPropertyChanged(nameof(HasMessage));
            }
        }
        public bool HasMessage => !string.IsNullOrEmpty(Message);
    }
}
