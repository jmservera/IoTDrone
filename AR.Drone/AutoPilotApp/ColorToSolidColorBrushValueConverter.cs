using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AutoPilotApp
{
    public class ConfigToGradientColorBrushValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length!=2)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            if (values[0] is Color && values[1] is Color)
            {
                return new LinearGradientBrush((Color)values[0], (Color) values[1], 0);
            }
            // You can support here more source types if you wish
            // For the example I throw an exception

            throw new InvalidOperationException("Unsupported types");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ColorToSolidColorBrushValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value)
            {
                return null;
            }
            // For a more sophisticated converter, check also the targetType and react accordingly..
            if (value is Color)
            {
                Color color = (Color)value;
                return new SolidColorBrush(color);
            }
            // You can support here more source types if you wish
            // For the example I throw an exception

            Type type = value.GetType();
            throw new InvalidOperationException("Unsupported type [" + type.Name + "]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // If necessary, here you can convert back. Check if which brush it is (if its one),
            // get its Color-value and return it.

            throw new NotImplementedException();
        }
    }
}
