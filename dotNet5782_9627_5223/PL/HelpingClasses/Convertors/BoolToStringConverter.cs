using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    public class BoolToStringConverter : IValueConverter
    {
        #region Convert&ConvertBack
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value? "Add" : "Update";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Add" : "Update";
        }
        #endregion
    }
}
