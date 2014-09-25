using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Rabid.SaverSetter
{
    [ValueConversion(typeof(Object[]), typeof(StringBuilder))]
    class RegistryScriptConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public Object Convert(Object[] pValues, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            SaverMenu qMenu = pParameter as SaverMenu;
            if (qMenu == null) { return null; }
            StringBuilder qBuilder = new StringBuilder(Properties.Resources.ScriptHeaderText);
            qBuilder.AppendLine();
            qBuilder.AppendLine();
            qBuilder.AppendLine("; " + qMenu.SaverName + " Screensaver Settings");
            qBuilder.AppendLine("[" + Properties.Resources.GlobalRegistryPath + @"\" + qMenu.SaverName + "]");
            foreach (DependencyPropertyDescriptor zDPD in qMenu.MenuOptions)
            {
                //      For each Property Name and Value
                //      Create a Value under the Saver RegistryKey
                String qValue = String.Empty;
                //      Convert Boolean to dword zero or dword one
                if (zDPD.PropertyType.Equals(typeof(Boolean)))
                { qValue = ((Boolean)zDPD.GetValue(qMenu)) ? "00000001" : "00000000"; }
                //      Convert Integer to dword hexadecimal
                else if (zDPD.PropertyType.Equals(typeof(Int32)))
                { qValue = ((Int32)zDPD.GetValue(qMenu)).ToString("X8", CultureInfo.InvariantCulture); }
                qBuilder.AppendLine("\"" + zDPD.Name + "\"=dword:" + qValue);
            }
            return qBuilder;
        }


        public Object[] ConvertBack(Object pValue, Type[] pTargetTypes, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
