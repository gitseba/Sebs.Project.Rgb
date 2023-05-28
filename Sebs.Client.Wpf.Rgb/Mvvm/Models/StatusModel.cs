
using Sebs.Client.Wpf.Rgb.Mvvm.ViewModel;

namespace Sebs.Client.Wpf.Rgb.Models
{
    /// <summary>
    /// Purpose: 
    /// Created by: Sebastian
    /// Created at: 3/24/2021 11:35:14 AM
    /// </summary>
    public class StatusModel : BaseModel
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
