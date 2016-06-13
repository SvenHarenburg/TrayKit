using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace TrayKit.Controls
{
  /// <summary>
  /// 
  /// </summary>
  /// <remarks>Source:https://social.msdn.microsoft.com/Forums/vstudio/en-US/3ee5696c-4f26-4e30-8891-0e2f95d69623/gridview-last-column-to-fill-available-space?forum=wpf</remarks>
  internal class WidthConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      ListView l = value as ListView;
      GridView g = l.View as GridView;
      double total = 0;
      for (int i = 0; i < g.Columns.Count - 1; i++)
      {
        total += g.Columns[i].ActualWidth;
      }
      return (l.ActualWidth - total);

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
