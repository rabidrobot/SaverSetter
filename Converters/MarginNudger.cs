using System;
using System.Windows;
using System.Windows.Data;

namespace Rabid.Conversion
{
    [ValueConversion(typeof(Thickness), typeof(Thickness))]
    internal class MarginNudger : IValueConverter
    {
        #region IValueConverter Members

        public Object Convert(Object pValue, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            Thickness qIn = (Thickness)pValue;
            if (Double.IsNaN(qIn.Top)) { qIn.Top = 0; }
            qIn.Top += 4;
            return qIn;
        }

        public Object ConvertBack(Object pValue, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            Thickness qIn = (Thickness)pValue;
            return new Thickness(qIn.Left, qIn.Top - (Double)pParameter, qIn.Right, qIn.Bottom);
        }

        #endregion
    }
}
