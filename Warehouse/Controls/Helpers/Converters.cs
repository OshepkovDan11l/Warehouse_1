using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WarehouseData.Models;

namespace Warehouse.Controls.Helpers
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Product p)
                return $"{p.Price:F2} ₽";
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class DiscountBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Product p && p.DiscountPercent > 0)
                return Brushes.LightYellow;
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}