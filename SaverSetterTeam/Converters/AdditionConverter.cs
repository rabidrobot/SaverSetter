using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Rabid.SaverSetter
{
    [ValueConversion(typeof(Double),typeof(Double))]
    internal class AdditionConverter : IValueConverter
    {
        #region IValueConverter Members

        public Object Convert(Object pValue, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            return (Double)pValue + (Double)pParameter;
        }

        public Object ConvertBack(Object pValue, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            return (Double)pValue - (Double)pParameter;
        }

        #endregion
    }
}
