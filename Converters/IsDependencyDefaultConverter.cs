using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;

namespace Rabid.SaverSetter
{
    [ValueConversion(typeof(Object[]), typeof(Boolean))]
    class IsDependencyDefaultConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        /// <summary>
        /// Converts whenever any of pValues is updated.
        /// Checks values of properties of pParameter against Default Values
        /// If any differ, the settings are not at default, return false.
        /// Any DependencyProperties that have null Default, must have null values,
        /// or return false, not default.
        /// </summary>
        /// <param name="pValues">Binding for each property that will trigger a check on change</param>
        /// <param name="pTargetType">Destination Type</param>
        /// <param name="pParameter">Object instance to examine</param>
        /// <param name="pCulture">Culture</param>
        /// <returns></returns>
        public Object Convert(Object[] pValues, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            Boolean qResult = true;
            //      Use BindingFlags to select public instance properties not inherited,
            //      the only ones checked against default values here.
            //      Reflection.PropertyInfo is result type
            IEnumerable<String> qNames = from PropertyInfo lPI in pParameter.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                         select lPI.Name;
            //      ComponentModel.PropertyDescriptor...
            foreach (PropertyDescriptor zPD in TypeDescriptor.GetProperties(pParameter, new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) }).OfType<PropertyDescriptor>().Where(lPD => qNames.Contains(lPD.Name)))
            {
                //      ...ComponentModel.DependencyPropertyDescriptor has Metadata
                DependencyPropertyDescriptor zDPD = DependencyPropertyDescriptor.FromProperty(zPD);
                if (zDPD == null) { continue; } //      Not a DependencyProperty
                //      Check for null mismatch
                if ((zDPD.Metadata.DefaultValue == null && zDPD.GetValue(pParameter) != null) || (zDPD.Metadata.DefaultValue != null && zDPD.GetValue(pParameter) == null)) { return false; }
                //      Check for value mismatch
                if (!zDPD.GetValue(pParameter).Equals(zDPD.Metadata.DefaultValue)) { return false; }
            }
            return qResult;
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pValue"></param>
        /// <param name="pTargetTypes"></param>
        /// <param name="pParameter"></param>
        /// <param name="pCulture"></param>
        /// <returns></returns>
        public Object[] ConvertBack(Object pValue, Type[] pTargetTypes, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
