using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace Rabid.SaverSetter
{
    /// <summary>
    /// Interaction logic for RibbonsMenu.xaml
    /// </summary>
    public partial class RibbonsMenu : SaverMenu
    {
        #region Properties
        
        public Int32 CameraFOV
        {
            get { return (Int32)GetValue(CameraFOVProperty); }
            set { SetValue(CameraFOVProperty, value); }
        }
        public static readonly DependencyProperty CameraFOVProperty =
            DependencyProperty.Register("CameraFOV", typeof(Int32), typeof(RibbonsMenu), new UIPropertyMetadata(1065000000, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 RibbonWidth
        {
            get { return (Int32)GetValue(RibbonWidthProperty); }
            set { SetValue(RibbonWidthProperty, value); }
        }
        public static readonly DependencyProperty RibbonWidthProperty =
            DependencyProperty.Register("RibbonWidth", typeof(Int32), typeof(RibbonsMenu), new UIPropertyMetadata(1010000000, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 NumRibbons
        {
            get { return (Int32)GetValue(NumRibbonsProperty); }
            set { SetValue(NumRibbonsProperty, value); }
        }
        public static readonly DependencyProperty NumRibbonsProperty =
            DependencyProperty.Register("NumRibbons", typeof(Int32), typeof(RibbonsMenu), new UIPropertyMetadata(8, new PropertyChangedCallback(SaverPropertyChanged)));

        public Boolean Blur
        {
            get { return (Boolean)GetValue(BlurProperty); }
            set { SetValue(BlurProperty, value); }
        }
        public static readonly DependencyProperty BlurProperty =
            DependencyProperty.Register("Blur", typeof(Boolean), typeof(RibbonsMenu), new UIPropertyMetadata(true, new PropertyChangedCallback(SaverPropertyChanged)));

        #endregion

        public RibbonsMenu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(RibbonsMenu_Loaded);
        }

        void RibbonsMenu_Loaded(object sender, RoutedEventArgs e)
        {
            SaverName = "Ribbons";
            SaverFile = "Ribbons";
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
