using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using Microsoft.Win32;

namespace Rabid.SaverSetter
{
    /// <summary>
    /// Interaction logic for MasterMenu.xaml
    /// </summary>
    public partial class MasterMenu : SaverMenu
    {
        public static IEnumerable<String> SaverNames
        {
            get;
            set;
        }

        public Int32 ScreenSaveTimeOut
        {
            get { return (Int32)GetValue(ScreenSaveTimeOutProperty); }
            set { SetValue(ScreenSaveTimeOutProperty, value); }
        }
        public static readonly DependencyProperty ScreenSaveTimeOutProperty =
            DependencyProperty.Register("ScreenSaveTimeOut", typeof(Int32), typeof(MasterMenu), new UIPropertyMetadata(new PropertyChangedCallback(SaverPropertyChanged)));

        public Boolean ScreenSaverIsSecure
        {
            get { return (Boolean)GetValue(ScreenSaverIsSecureProperty); }
            set { SetValue(ScreenSaverIsSecureProperty, value); }
        }
        public static readonly DependencyProperty ScreenSaverIsSecureProperty =
            DependencyProperty.Register("ScreenSaverIsSecure", typeof(Boolean), typeof(MasterMenu), new UIPropertyMetadata(false, new PropertyChangedCallback(SaverPropertyChanged)));

        public Boolean ScreenSaveActive
        {
            get { return (Boolean)GetValue(ScreenSaveActiveProperty); }
            set { SetValue(ScreenSaveActiveProperty, value); }
        }
        public static readonly DependencyProperty ScreenSaveActiveProperty =
            DependencyProperty.Register("ScreenSaveActive", typeof(Boolean), typeof(MasterMenu), new UIPropertyMetadata(true, new PropertyChangedCallback(SaverPropertyChanged)));

        internal static String GetCurrentScreenSaverName()
        {
            String qString = (String)Registry.CurrentUser.OpenSubKey(Properties.Resources.MasterRegistryPath, false).GetValue("SCRNSAVE.EXE");
            if (String.IsNullOrEmpty(qString)) { qString = String.Empty; }
            else
            {
                //      Get filename of current enabled screensaver from full path
                qString = qString.Split('\\')[qString.Split('\\').Length - 1].Split('.')[0];
            }
            return qString;
        }

        static MasterMenu()
        {
            DirectoryInfo qDirInfo = new DirectoryInfo(System.Environment.GetFolderPath(System.Environment.SpecialFolder.System));
            SaverNames = qDirInfo.GetFiles("*.scr").Select(fi => fi.Name.Split('.')[0]);
        }

        public MasterMenu()
        {
            RegKey = Registry.CurrentUser.OpenSubKey(Properties.Resources.MasterRegistryPath, true);
            Commands.TweakScrCommands.BindCommandSet(this);
            ScreenSaveTimeOut = Convert.ToInt32(RegKey.GetValue(ScreenSaveTimeOutProperty.Name), CultureInfo.InvariantCulture);
            ScreenSaverIsSecure = (Convert.ToInt32(RegKey.GetValue(ScreenSaverIsSecureProperty.Name)) == 1) ? true : false;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MasterMenu_Loaded);
        }

        void MasterMenu_Loaded(object sender, RoutedEventArgs e)
        {
            xComboBox.SelectedItem = SaverSetterMain.RunningSaverName;
            Binding qBind = new Binding(ScreenSaveTimeOutProperty.Name);
            qBind.Source = this;
            qBind.Converter = new IntegerWithBoolean();
            this.SetBinding(ScreenSaveActiveProperty, qBind);
            this.Changes = false;
        }

        private void xComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!xComboBox.SelectedValue.Equals(SaverSetterMain.RunningSaverName))
            {
                SaverSetterMain.RunningSaverName = (String)xComboBox.SelectedValue;
                (Application.Current.MainWindow as SaverSetterMain).UpdatePreviewWindow();
                this.Changes = true;
            }
        }

        private void Preview_Clicked(object sender, RoutedEventArgs e)
        {
            Process qPreview = new Process();
            qPreview.StartInfo.UseShellExecute = false;
            qPreview.StartInfo.Arguments = "/s";
            String qCommandLine = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + xComboBox.SelectedValue + ".scr";
            qPreview.StartInfo.FileName = qCommandLine;
            qPreview.Start();
        }

        private void Settings_Clicked(object sender, RoutedEventArgs e)
        {
            OptionsWindow qOpts = null;
            qOpts = new OptionsWindow();
            qOpts.Top = Application.Current.MainWindow.Top + 32;
            qOpts.Left = Application.Current.MainWindow.Left - 32;
            qOpts.Topmost = true;
            switch ((String)xComboBox.SelectedValue)
            {
                case "Aurora":
                case "Aurora screensaver":
                    AuroraMenu qAurora = new AuroraMenu();
                    qAurora.RegistryUpdated += new RoutedEventHandler((Application.Current.MainWindow as SaverSetterMain).SaverMenu_RegistryUpdated);
                    qOpts.Title = "Aurora";
                    qOpts.Content = qAurora;
                    qOpts.Show();
                    break;
                case "Bubbles":
                    BubblesMenu qBubbles = new BubblesMenu();
                    qBubbles.RegistryUpdated += new RoutedEventHandler((Application.Current.MainWindow as SaverSetterMain).SaverMenu_RegistryUpdated);
                    qOpts.Title = "Bubbles";
                    qOpts.Content = qBubbles;
                    qOpts.Show();
                    break;
                case "Mystify":
                    MystifyMenu qMystify = new MystifyMenu();
                    qMystify.RegistryUpdated += new RoutedEventHandler((Application.Current.MainWindow as SaverSetterMain).SaverMenu_RegistryUpdated);
                    qOpts.Title = "Mystify";
                    qOpts.Content = qMystify;
                    qOpts.Show();
                    break;
                case "Ribbons":
                    RibbonsMenu qRibbons = new RibbonsMenu();
                    qRibbons.RegistryUpdated += new RoutedEventHandler((Application.Current.MainWindow as SaverSetterMain).SaverMenu_RegistryUpdated);
                    qOpts.Title = "Ribbons";
                    qOpts.Content = qRibbons;
                    qOpts.Show();
                    break;
                default:
                    Process qPreview = new Process();
                    qPreview.StartInfo.UseShellExecute = false;
                    qPreview.StartInfo.Arguments = "/c";
                    String qCommandLine = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + xComboBox.SelectedValue + ".scr";
                    qPreview.StartInfo.FileName = qCommandLine;
                    qPreview.Start();
                    break;
            }
        }

        private void Apply_Clicked(object sender, RoutedEventArgs e)
        {
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            //xComboBox.SelectedValue = SaverSetterMain.RunningSaverName;
            //this.Changes = false;
        }

        private void OK_Clicked(object sender, RoutedEventArgs e)
        {
            //String qStr = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + xComboBox.SelectedValue + ".scr";
            //RegKey.SetValue("SCRNSAVE.EXE", qStr);
            Application.Current.Shutdown();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                String qStr = (sender as System.Windows.Documents.Hyperlink).NavigateUri.ToString();
                Process.Start(new ProcessStartInfo(qStr));
                e.Handled = true;
            }
            catch (Exception qErr) { Debug.WriteLine("Uri Navigation Failed: " + qErr.ToString()); throw; }
        }

        public void WriteSaverSettings()
        {
            // base.WriteSaverSettings();
            String qStr = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + xComboBox.SelectedValue + ".scr";
            RegKey.SetValue("SCRNSAVE.EXE", qStr);
            RegKey.SetValue(ScreenSaverIsSecureProperty.Name, ScreenSaverIsSecure ? Convert.ToString(1, CultureInfo.InvariantCulture) : Convert.ToString(0, CultureInfo.InvariantCulture));
            RegKey.SetValue(ScreenSaveActiveProperty.Name, ScreenSaveActive ? "1" : "0");
            // Decimal, not hex like defaults
            RegKey.SetValue(ScreenSaveTimeOutProperty.Name, Convert.ToString(ScreenSaveTimeOut, CultureInfo.InvariantCulture));
            this.Changes = false;
        }
    }
}
