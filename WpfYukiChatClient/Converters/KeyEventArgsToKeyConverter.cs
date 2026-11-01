using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfYukiChatClient.Converters;

public class KeyEventArgsToKeyConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is KeyEventArgs keyEventArgs ? keyEventArgs.Key : Binding.DoNothing;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
