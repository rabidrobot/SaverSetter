using System;
using System.Diagnostics;
using System.Windows;


namespace Rabid.SaverSetter
{

    public partial class SaverSetterMain : System.Windows.Window
    {
        internal static Process RunningSaver
        {
            get;
            set;
        }

        internal static String RunningSaverName
        {
            get;
            set;
        }

        ScrPreviewWindow PreviewWindow { get; set; }

        static SaverSetterMain()
        {
            RunningSaverName = (MasterMenu.GetCurrentScreenSaverName().Equals(String.Empty)) ? "Bubbles" : MasterMenu.GetCurrentScreenSaverName();
        }

        public SaverSetterMain()
        {
            CloseRunningSavers();
            InitializeComponent();
            if (!this.AllowsTransparency)
            { this.SourceInitialized += new EventHandler(delegate(Object pSender, EventArgs pArgs) { DwmHelper.ExtendGlassFrame(this, new Thickness(-1)); }); }
            this.Loaded += new RoutedEventHandler(SaverSetterMain_Loaded);
            this.Closed += new EventHandler(SaverSetterMain_Closed);
        }


        private void CloseRunningSavers()
        {
            foreach (Process zP in Process.GetProcesses())
            {
                if (zP.ProcessName.EndsWith("scr", StringComparison.CurrentCultureIgnoreCase))
                { zP.Kill(); }
            }
        }
        internal void SaverSetterMain_Closed(object sender, EventArgs e)
        {
            if (!RunningSaver.HasExited)
            { RunningSaver.Kill(); }
            Application.Current.Shutdown();
        }

        void SaverSetterMain_Loaded(object obj, RoutedEventArgs e)
        {
            this.Left = (System.Windows.SystemParameters.MaximizedPrimaryScreenWidth - this.ActualWidth) / 2;
            this.Top = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight - (this.ActualHeight * 1.5) - (System.Windows.SystemParameters.MaximizedPrimaryScreenHeight * 0.3);
            PreviewWindow = new ScrPreviewWindow();
            PreviewWindow.Show();
            (PreviewWindow as FrameworkElement).SizeChanged += new SizeChangedEventHandler(PreviewWindow_SizeChanged);
            UpdatePreviewWindow();
        }

        void PreviewWindow_SizeChanged(Object pSender, SizeChangedEventArgs pArgs)
        {
            if (!RunningSaver.HasExited)
            { RunningSaver.Kill(); }
            RunningSaver.Start();
        }

        internal void UpdatePreviewWindow()
        {
            if (RunningSaver != null)
            {
                if (!RunningSaver.HasExited) { RunningSaver.Kill(); }
            }
            RunningSaver = new Process();
            RunningSaver.StartInfo.UseShellExecute = false;
            RunningSaver.StartInfo.Arguments = "/p " + PreviewWindow.PreviewHwnd;
            String qCommandLine = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\" + RunningSaverName + ".scr";
            RunningSaver.StartInfo.FileName = qCommandLine;
            RunningSaver.Start();
        }


        private void SaverSetterMain_MouseDown(object sender, RoutedEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }

        internal void SaverMenu_RegistryUpdated(Object pSender, RoutedEventArgs pArgs)
        {
            RunningSaverName = (pSender as SaverMenu).SaverFile;
            UpdatePreviewWindow();
        }
    }
}