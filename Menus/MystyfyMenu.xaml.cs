using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace Rabid.SaverSetter
{
    /// <summary>
    /// Interaction logic for MystifyMenu.xaml
    /// </summary>
    public partial class MystifyMenu : SaverMenu
    {
        public Int32 CameraFOV
        {
            get { return (Int32)GetValue(CameraFOVProperty); }
            set { SetValue(CameraFOVProperty, value); }
        }
        public static readonly DependencyProperty CameraFOVProperty =
            DependencyProperty.Register("CameraFOV", typeof(Int32), typeof(MystifyMenu), new UIPropertyMetadata(0x3F52BBAF, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 LineWidth
        {
            get { return (Int32)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }
        public static readonly DependencyProperty LineWidthProperty =
            DependencyProperty.Register("LineWidth", typeof(Int32), typeof(MystifyMenu), new UIPropertyMetadata(0x3C6C309A, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 NumLines
        {
            get { return (Int32)GetValue(NumLinesProperty); }
            set { SetValue(NumLinesProperty, value); }
        }
        public static readonly DependencyProperty NumLinesProperty =
            DependencyProperty.Register("NumLines", typeof(Int32), typeof(MystifyMenu), new UIPropertyMetadata(8, new PropertyChangedCallback(SaverPropertyChanged)));

        public MystifyMenu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MystifyMenu_Loaded);
        }

        void MystifyMenu_Loaded(object sender, RoutedEventArgs e)
        {
            SaverName = "Mystify";
            SaverFile = "Mystify";
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
