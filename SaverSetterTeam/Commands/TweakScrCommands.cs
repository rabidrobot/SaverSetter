using System;
using System.Windows;
using System.Windows.Input;

namespace Rabid.SaverSetter.Commands
{
    /// <summary>
    /// Commands for the SaverMenus
    /// </summary>
    public static class TweakScrCommands
    {
        #region Construct and Bind

        static TweakScrCommands()
        {
            __Apply = new RoutedUICommand("Apply the new settings", "Apply", typeof(TweakScrCommands));
            __Cancel = new RoutedUICommand("Close without applying new changes", "Cancel", typeof(TweakScrCommands));
            __Defaults = new RoutedUICommand("Set to demonstration settings", "Sample", typeof(TweakScrCommands));
            __ToClipboard = new RoutedUICommand("Copy settings to clipboard", "Clipboard", typeof(TweakScrCommands));
            __Unset = new RoutedUICommand("Reset to factory-original settings", "Unset", typeof(TweakScrCommands));
            __OK = new RoutedUICommand("Apply new settings and close panel", "OK", typeof(TweakScrCommands));
        }

        public static void BindCommandSet(UIElement pUIElement)
        {
            pUIElement.CommandBindings.Add(new CommandBinding(OK, OK_Executed, OK_CanExecute));
            pUIElement.CommandBindings.Add(new CommandBinding(Unset, Unset_Executed, Unset_CanExecute));
            pUIElement.CommandBindings.Add(new CommandBinding(ToClipboard, ToClipboard_Executed, ToClipboard_CanExecute));
            pUIElement.CommandBindings.Add(new CommandBinding(Defaults, Defaults_Executed, Defaults_CanExecute));
            pUIElement.CommandBindings.Add(new CommandBinding(Cancel, Cancel_Executed, Cancel_CanExecute));
            pUIElement.CommandBindings.Add(new CommandBinding(Apply, Apply_Executed, Apply_CanExecute));
        }

        #endregion

        #region Apply Changes Command

        private static RoutedUICommand __Apply;
        public static RoutedUICommand Apply
        { get { return __Apply; } }
        private static void Apply_Executed(Object pSender, ExecutedRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            if (qMenu != null)
            { qMenu.WriteSaverSettings(); }
        }
        private static void Apply_CanExecute(Object pSender, CanExecuteRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            if (qMenu != null)
            { pArgs.CanExecute = (qMenu.Changes || qMenu.RegKey.ValueCount == 0); }
            else
            { pArgs.CanExecute = false; }
        }

        #endregion

        #region Cancel Changes Command

        private static RoutedUICommand __Cancel;
        public static RoutedUICommand Cancel
        { get { return __Cancel; } }
        private static void Cancel_Executed(Object pSender, ExecutedRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            FrameworkElement qFE = pSender as FrameworkElement;
            while (qFE.Parent != null)
            {
                qFE = (FrameworkElement)qFE.Parent;
            }
            (qFE as Window).Close();
        }
        private static void Cancel_CanExecute(Object pSender, CanExecuteRoutedEventArgs pArgs)
        {
            pArgs.CanExecute = true;
        }

        #endregion

        #region Defaults Command

        private static RoutedUICommand __Defaults;
        public static RoutedUICommand Defaults
        { get { return __Defaults; } }
        private static void Defaults_Executed(Object pSender, ExecutedRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            if (qMenu != null)
            { qMenu.DefaultSaverSettings(); }
        }
        private static void Defaults_CanExecute(Object pSender, CanExecuteRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            pArgs.CanExecute = (qMenu == null) ? false : (Attachable.GetIsNotDependencyDefault(qMenu) || qMenu.RegKey.ValueCount==0);
            //if (qMenu != null)
            //{
            //    pArgs.CanExecute = Attachable.GetIsNotDependencyDefault(qMenu);
            //}
            //else
            //{ pArgs.CanExecute = false; }
        }

        #endregion

        #region ToClipboard Command

        private static RoutedUICommand __ToClipboard;
        public static RoutedUICommand ToClipboard
        { get { return __ToClipboard; } }
        private static void ToClipboard_Executed(Object pSender, ExecutedRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            if (qMenu != null)
            { Clipboard.SetText(qMenu.RegistryScript.ToString()); }
        }
        private static void ToClipboard_CanExecute(Object pSender, CanExecuteRoutedEventArgs pArgs)
        {
            pArgs.CanExecute = true;
        }

        #endregion

        #region Unset Command

        private static RoutedUICommand __Unset;
        public static RoutedUICommand Unset
        { get { return __Unset; } }
        private static void Unset_Executed(Object pSender, ExecutedRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            if (qMenu != null)
            { qMenu.UnsetSaverSettings(); }
            FrameworkElement qFE = pSender as FrameworkElement;
            while (qFE.Parent != null)
            {
                qFE = (FrameworkElement)qFE.Parent;
            }
            (qFE as Window).Close();
        }
        private static void Unset_CanExecute(Object pSender, CanExecuteRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            if (qMenu == null) { return; }
            pArgs.CanExecute = (qMenu.RegKey.ValueCount > 0) ? true : false;
            //if (qMenu.RegKey.ValueCount > 0) { pArgs.CanExecute = true; }
            //else { pArgs.CanExecute = false; }
        }

        #endregion

        #region OK Changes Command

        private static RoutedUICommand __OK;
        public static RoutedUICommand OK
        { get { return __OK; } }
        private static void OK_Executed(Object pSender, ExecutedRoutedEventArgs pArgs)
        {
            SaverMenu qMenu = pSender as SaverMenu;
            if (qMenu != null)
            { qMenu.WriteSaverSettings(); }
            FrameworkElement qFE = pSender as FrameworkElement;
            while (qFE.Parent != null)
            {
                qFE = (FrameworkElement)qFE.Parent;
            }
            (qFE as Window).Close();
        }
        private static void OK_CanExecute(Object pSender, CanExecuteRoutedEventArgs pArgs)
        {
            pArgs.CanExecute = true;
        }

        #endregion
    }
}
