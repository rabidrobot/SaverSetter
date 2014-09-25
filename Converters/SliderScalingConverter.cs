using System;
using System.Windows.Data;

namespace Rabid.Conversion
{
    /// <summary>
    /// Converts minimum, maximum, and value to value percentage of maximum less minimum
    /// </summary>
    [ValueConversion(typeof(Object[]), typeof(Int32))]
    internal class RelativeValueConverter : IMultiValueConverter
    {

        #region IMultiValueConverter Members

        public object Convert(Object[] pValues, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            if (pValues.Length != 3) { return Double.NaN; }
            Double qMaximum = Double.NaN
                , qMinimum = Double.NaN
                , qValue = Double.NaN;
            Boolean qBool = true;
            qBool = Double.TryParse(pValues[0].ToString(), out qMaximum);
            if (qBool) { qBool = Double.TryParse(pValues[1].ToString(), out qMinimum); }
            if (qBool) { qBool = Double.TryParse(pValues[2].ToString(), out qValue); }
            return (qBool && qMinimum != qMaximum) ? (Int32)(100.0 - ((qMaximum - qValue) / (qMaximum - qMinimum)) * 100.0) : Double.NaN;
        }

        public object[] ConvertBack(Object pValue, Type[] pTargetTypes, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            throw new NotImplementedException(GetType().Name + " does not implement ConvertBack");
        }

        #endregion
    }
}
