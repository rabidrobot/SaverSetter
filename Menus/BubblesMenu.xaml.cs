using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace Rabid.SaverSetter
{
    /// <summary>
    /// Interaction logic for BubblesMenu.xaml
    /// </summary>
    public partial class BubblesMenu : SaverMenu
    {
        #region Bubbles Properties

        //public override RegistryKey RegKey { get { return Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Screensavers\Bubbles", true); } }

        //    Access to settings for Bubbles.scr
        //    HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Screensavers\Bubbles
        //    MaterialGlass         [Boolean]     0=Transparent glass bubbles, 1=Opaque metallic bubbles
        //    ShowShadows           [Boolean]     1 enables shadows from bubbles, 0 disables
        //    ShowBubbles           [Boolean]     0 uses Black Background, 1 uses Desktop
        //    Radius                [Integer]     Between 1,090,000,000 and 1,130,000,000 Smaller means more bubbles, larger means bigger bubbles
        //    TurbulenceNumOctaves  [Integer]     Values 1-255 set color cycling rate
        //    SphereDensity         [Integer]     Between 1,000,000,000 and 2,100,000,000
        //    These settings not given controls
        //    TurbulenceForce       [Integer]     Unknown Effect ~1,000,000,000 and up
        //    TurbulenceSpeed       [Integer]     Unknown Effect
        //    BMP                   [String(?)]   Unknown Effect

        public Boolean MaterialGlass
        {
            get { return (Boolean)GetValue(MaterialGlassProperty); }
            set { SetValue(MaterialGlassProperty, value); }
        }
        public static readonly DependencyProperty MaterialGlassProperty =
            DependencyProperty.Register("MaterialGlass", typeof(Boolean), typeof(BubblesMenu), new UIPropertyMetadata(false, new PropertyChangedCallback(SaverPropertyChanged)));

        public Boolean ShowShadows
        {
            get { return (Boolean)GetValue(ShowShadowsProperty); }
            set { SetValue(ShowShadowsProperty, value); }
        }
        public static readonly DependencyProperty ShowShadowsProperty =
            DependencyProperty.Register("ShowShadows", typeof(Boolean), typeof(BubblesMenu), new UIPropertyMetadata(true, new PropertyChangedCallback(SaverPropertyChanged)));

        public Boolean ShowBubbles
        {
            get { return (Boolean)GetValue(ShowBubblesProperty); }
            set { SetValue(ShowBubblesProperty, value); }
        }
        public static readonly DependencyProperty ShowBubblesProperty =
            DependencyProperty.Register("ShowBubbles", typeof(Boolean), typeof(BubblesMenu), new UIPropertyMetadata(true, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 Radius
        {
            get { return (Int32)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(Int32), typeof(BubblesMenu), new UIPropertyMetadata(0x431F9EA8, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 TurbulenceNumOctaves
        {
            get { return (Int32)GetValue(TurbulenceNumOctavesProperty); }
            set { SetValue(TurbulenceNumOctavesProperty, value); }
        }
        public static readonly DependencyProperty TurbulenceNumOctavesProperty =
            DependencyProperty.Register("TurbulenceNumOctaves", typeof(Int32), typeof(BubblesMenu), new UIPropertyMetadata(14, new PropertyChangedCallback(SaverPropertyChanged)));

        public Int32 SphereDensity
        {
            get { return (Int32)GetValue(SphereDensityProperty); }
            set { SetValue(SphereDensityProperty, value); }
        }
        public static readonly DependencyProperty SphereDensityProperty =
            DependencyProperty.Register("SphereDensity", typeof(Int32), typeof(BubblesMenu), new UIPropertyMetadata(0x3E76702B, new PropertyChangedCallback(SaverPropertyChanged)));
      
        #endregion

        public BubblesMenu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(BubblesMenu_Loaded);
        }

        void BubblesMenu_Loaded(object sender, RoutedEventArgs e)
        {
            SaverName = "Bubbles";
            SaverFile = "Bubbles";
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
