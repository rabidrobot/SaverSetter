using System;
using System.Windows;

namespace Rabid.SaverSetter
{
    public static class Attachable
    {

        /// <remarks>
        /// SetBinding in the setup requires FrameworkElement 
        /// </remarks>
        #region IsDependencyDefault & IsNotDependencyDefault Attached Properties

        public static Boolean? GetIsDependencyDefault(FrameworkElement pFrameworkElement)
        {
            //  If null, then set up bindings
            if (pFrameworkElement.GetValue(IsDependencyDefaultProperty)==null)
            {
                IsDependencyDefaultBinding.CreateIsDependencyDefaultBinding(pFrameworkElement);
            }
            return (Boolean)pFrameworkElement.GetValue(IsDependencyDefaultProperty);
        }
        internal static void SetIsDependencyDefault(FrameworkElement  pFrameworkElement, Boolean pValue)
        {
            pFrameworkElement.SetValue(IsDependencyDefaultProperty, pValue);
        }
        public static readonly DependencyProperty IsDependencyDefaultProperty =
            DependencyProperty.RegisterAttached("IsDependencyDefault", typeof(Boolean?), typeof(FrameworkElement), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(IsDependencyDefaultChanged)));

        public static Boolean GetIsNotDependencyDefault(FrameworkElement pFrameworkElement)
        {
            return !(Boolean)GetIsDependencyDefault(pFrameworkElement);
        }
        internal static void SetIsNotDependencyDefault(FrameworkElement  pFrameworkElement, Boolean pValue)
        {
            pFrameworkElement.SetValue(IsNotDependencyDefaultProperty, pValue);
        }
        public static readonly DependencyProperty IsNotDependencyDefaultProperty =
            DependencyProperty.RegisterAttached("IsNotDependencyDefault", typeof(Boolean), typeof(FrameworkElement));

        internal static void IsDependencyDefaultChanged(Object pObject, DependencyPropertyChangedEventArgs pArgs)
        {
            //      Update IsNotDependencyDefault when IsDependencyDefault changes
            FrameworkElement qFE = pObject as FrameworkElement;
            if (qFE != null && pArgs.NewValue != null)
            { Attachable.SetIsNotDependencyDefault(qFE, !(Boolean)pArgs.NewValue); }
        }

        #endregion
    }
}
