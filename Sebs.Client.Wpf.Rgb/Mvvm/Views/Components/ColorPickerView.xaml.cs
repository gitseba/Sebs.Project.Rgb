using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sebs.Client.Wpf.Rgb.Mvvm.Views.Components
{
    public partial class ColorPickerView : UserControl
    {
        public static readonly DependencyProperty RedProperty =
             DependencyProperty.Register(nameof(Red), typeof(byte), typeof(ColorPickerView),
                 new FrameworkPropertyMetadata((byte)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, RgbPropertyChanged));

        public static readonly DependencyProperty GreenProperty =
            DependencyProperty.Register(nameof(Green), typeof(byte), typeof(ColorPickerView),
               new FrameworkPropertyMetadata((byte)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, RgbPropertyChanged));

        public static readonly DependencyProperty BlueProperty =
            DependencyProperty.Register(nameof(Blue), typeof(byte), typeof(ColorPickerView),
               new FrameworkPropertyMetadata((byte)0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, RgbPropertyChanged));

        public static readonly DependencyProperty ColorBrushProperty =
           DependencyProperty.Register(nameof(ColorBrush), typeof(Brush), typeof(ColorPickerView),
               new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black)));

        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        public Brush ColorBrush
        {
            get { return (Brush)GetValue(ColorBrushProperty); }
            set { SetValue(ColorBrushProperty, value); }
        }

        public ColorPickerView()
        {
            InitializeComponent();
        }

        private static void RgbPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPickerView colorPicker)
            {
                colorPicker.UpdateColorBrush();
            }
        }

        private void UpdateColorBrush()
        {
            ColorBrush = new SolidColorBrush(Color.FromRgb(Red, Green, Blue));
        }
    }
}