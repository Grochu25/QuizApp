using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace QuizCreator.ViewModels.Converters
{
    internal class AnwserConverter : IValueConverter
    {
        int x = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || parameter is null) return false;
            sbyte a = (sbyte)value;
            sbyte b = sbyte.Parse(parameter.ToString());
            int c = a & b;
            return c == b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageBox.Show(parameter.ToString());
            x += int.Parse(parameter.ToString());
            return x;
        }
    }
}
