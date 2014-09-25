using System;
using System.Windows.Data;

namespace Rabid.SaverSetter
{
    [ValueConversion(typeof(Int32),typeof(Int32))]
    internal class SecondsWithMinutesConverter : IValueConverter
    {
        #region IValueConverter Members

        public Object Convert(Object pValue, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            return (Int32)pValue / 60;
        }

        public Object ConvertBack(Object pValue, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            //Int32 qI = (Int32)pValue;
            return 60 * (Double)pValue;
        }

        #endregion
    }
}
