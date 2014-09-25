using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace Rabid.SaverSetter
{
    /// <summary>
    /// Interaction logic for AuroraMenu.xaml
    /// </summary>
    public partial class AuroraMenu : SaverMenu
    {
        public Int32 Amplitude
        {
            get { return (Int32)GetValue(AmplitudeProperty); }
            set { SetValue(AmplitudeProperty, value); }
        }
        public static readonly DependencyProperty AmplitudeProperty =
            DependencyProperty.Register("Amplitude", typeof(Int32), typeof(AuroraMenu), new UIPropertyMetadata(0x3F4CD6D0, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 Brightness
        {
            get { return (Int32)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }
        public static readonly DependencyProperty BrightnessProperty =
            DependencyProperty.Register("Brightness", typeof(Int32), typeof(AuroraMenu), new UIPropertyMetadata(0x3E32A129, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 NumLayers
        {
            get { return (Int32)GetValue(NumLayersProperty); }
            set { SetValue(NumLayersProperty, value); }
        }
        public static readonly DependencyProperty NumLayersProperty =
            DependencyProperty.Register("NumLayers", typeof(Int32), typeof(AuroraMenu), new UIPropertyMetadata(2, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 Speed
        {
            get { return (Int32)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(Int32), typeof(AuroraMenu), new UIPropertyMetadata(0x3AFFB6B1, new PropertyChangedCallback(SaverPropertyChanged)));
        
        public AuroraMenu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(AuroraMenu_Loaded);
        }

        void AuroraMenu_Loaded(object sender, RoutedEventArgs e)
        {
            SaverName = "Aurora";
            SaverFile = "Aurora screensaver";
            if (Registry.CurrentUser.OpenSubKey(Properties.Resources.LocalRegistryPath).GetSubKeyNames().Contains(SaverName))
            {
                RegKey = Registry.CurrentUser.OpenSubKey(Properties.Resources.LocalRegistryPath + @"\" + SaverName, true);
                LoadSaverSettings();
                Commands.TweakScrCommands.BindCommandSet(this);
                RegistryScriptBinding.CreateRegistryScriptBinding(this);
            }
            else
            { (this.Parent as UIElement).Visibility = Visibility.Collapsed; }
        }

    }
}
