using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfYukiChatClient.Converters;

public class ConnectIndicatorConverter : IValueConverter {
    public required Brush ConnectBrush { get; set; }
    public required Brush DisconnectBrush { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is bool isConnected)
            return isConnected ? ConnectBrush : DisconnectBrush;
        return DisconnectBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
