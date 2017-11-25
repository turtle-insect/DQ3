using System;
using System.Globalization;
using System.Windows.Data;

namespace DQ3
{
	class OrderChoiceConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (uint)value - 1;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (uint)(int)value + 1;
		}
	}
}
