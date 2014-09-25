using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace Rabid.SaverSetter
{
    /// <summary>
    /// Multibinding required for attached property Attachable.IsDependencyDefault
    /// </summary>
    class IsDependencyDefaultBinding : MultiBinding
    {
        private IsDependencyDefaultBinding() { }

        /// <summary>
        /// Sets up the Multibinding required for the attached property Attachable.IsDependencyDefault
        /// </summary>
        /// <param name="pFrameworkElement"></param>
        public static void CreateIsDependencyDefaultBinding(FrameworkElement pFrameworkElement)
        {
            IsDependencyDefaultBinding qMulti = new IsDependencyDefaultBinding();
            //      Collect valid Property names
            IEnumerable<String> qNames = from PropertyInfo lPI in pFrameworkElement.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                         select lPI.Name;
            //      For each public Property declared at instance level
            foreach (PropertyDescriptor zPD in TypeDescriptor.GetProperties(pFrameworkElement, new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) }).OfType<PropertyDescriptor>().Where(lPI => qNames.Contains(lPI.Name)))
            {
                //      Add binding if it is a DependencyProperty
                DependencyPropertyDescriptor zDPD = DependencyPropertyDescriptor.FromProperty(zPD);
                if (zDPD == null) { continue; }
                //      New Binding with path from property name
                Binding zBinding = new Binding(zPD.Name);
                //      Source will be the instance
                zBinding.Source = pFrameworkElement;
                //      Add to collection
                qMulti.Bindings.Add(zBinding);
            }
            qMulti.Converter = new IsDependencyDefaultConverter();
            qMulti.ConverterParameter = pFrameworkElement;
            pFrameworkElement.SetBinding(Attachable.IsDependencyDefaultProperty, qMulti);
        }
    }
}
