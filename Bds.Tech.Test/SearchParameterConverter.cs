using Bds.Tech.Test.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bds.Tech.Test
{
    public class SearchParameterConverter : System.Windows.Data.IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Tuple<ObservableCollection<Result>, string> tuple = new Tuple<ObservableCollection<Result>, string>(
            (ObservableCollection<Result>)values[1], (string)values[0]);
            return tuple;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
