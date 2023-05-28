using Sebs.Client.Wpf.Rgb.Features;
using System.Windows;

namespace Sebs.Client.Wpf.Rgb
{
    /// <summary>
    /// Purpose: 
    /// Created by: sebde
    /// Created at: 5/28/2023 11:12:10 PM
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();

            //When window is resized, without this class,it's going over the screen edges.
            //This class keeps the window in the screen.
            //reference: 2:02:30 => https://youtu.be/TDOxHx-AMqQ?t=7350
            _ = new WindowResizer(this);
        }
    }
}
