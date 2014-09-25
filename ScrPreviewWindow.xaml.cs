using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Rabid.SaverSetter
{
    public partial class ScrPreviewWindow : Window
    {
        public IntPtr PreviewHwnd
        {
            get { return new WindowInteropHelper(this).Handle; }
        }

        public ScrPreviewWindow()
        {
            InitializeComponent();
            this.Title = "[SaverSetter] Resizable Preview";
            this.Loaded += new RoutedEventHandler(ScrPreviewWindow_Loaded);
        }

        void ScrPreviewWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = (Int32)(System.Windows.SystemParameters.VirtualScreenWidth * 0.3);
            this.Height = (Int32)(System.Windows.SystemParameters.VirtualScreenHeight * 0.3);
            this.Left = Application.Current.MainWindow.Left + Application.Current.MainWindow.ActualWidth / 2 - this.Width / 2;
            this.Top = Application.Current.MainWindow.Top + Application.Current.MainWindow.ActualHeight - 16;
            SaverSetterMain qMain = Application.Current.MainWindow as SaverSetterMain;
            if (qMain == null)
            { return; }
            this.Closed += new EventHandler(qMain.SaverSetterMain_Closed);
        }

        internal void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }

    }
}
