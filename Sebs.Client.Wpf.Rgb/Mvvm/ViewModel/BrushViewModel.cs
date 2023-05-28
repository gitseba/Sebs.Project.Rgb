using Sebs.Client.Wpf.Rgb.Models;
using System;
using System.Windows.Media;

namespace Sebs.Client.Wpf.Rgb.Mvvm.ViewModel
{
    /// <summary>
    /// Purpose: 
    /// Created by: Sebastian
    /// Created at: 3/24/2021 10:59:39 AM
    /// </summary>
    public class BrushViewModel : BaseViewModel
    {
        public RgbModel Color { get; set; }

        public Brush ColorBrush
        {
            get
            {
                try
                {
                    return new SolidColorBrush(System.Windows.Media.Color.FromRgb(Color.Red, Color.Green, Color.Blue));
                }
                catch (FormatException)
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }
        }

        public BrushViewModel(RgbModel color)
        {
            Color = color;
        }
    }
}
