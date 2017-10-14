using System;
using System.Globalization;
using System.Windows.Data;

namespace DQ3
{
	class SexConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint sex = (uint)value;
			return sex == 1 ? 0 : 1;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint sex = (uint)(int)value;
			return sex == 0 ? 1 : 9;
		}
	}
}
