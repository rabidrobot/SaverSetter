using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Rabid.SaverSetter
{
    [ValueConversion(typeof(Int32),typeof(Boolean))]
    internal class IntegerWithBoolean : IValueConverter
    {
        #region IValueConverter Members

        public Object Convert(Object pValue, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            return ((Int32)pValue == 0) ? false : true;
        }

        public Object ConvertBack(Object pValue, Type pTargetType, Object pParameter, System.Globalization.CultureInfo pCulture)
        {
            return ((Boolean)pValue) ? 1 : 0;
        }

        #endregion
    }
}
