using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Rabid.SaverSetter
{
    public partial class OptionsWindow : Window
    {
        public IntPtr PreviewHwnd
        {
            get { return new WindowInteropHelper(this).Handle; }
        }

        public OptionsWindow()
        {
            this.SourceInitialized += new EventHandler(delegate(Object pSender, EventArgs pArgs) { DwmHelper.ExtendGlassFrame(this, new Thickness(-1)); });
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(OptionsWindow_Loaded);
        }

        public OptionsWindow(String pTitle)
        {
            this.SourceInitialized += new EventHandler(delegate(Object pSender, EventArgs pArgs) { DwmHelper.ExtendGlassFrame(this, new Thickness(-1)); });
            InitializeComponent();
            this.Title = pTitle;
            this.Loaded += new RoutedEventHandler(OptionsWindow_Loaded);
        }

        void OptionsWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void OptionsWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }

    }
}
