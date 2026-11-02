using System.Windows;
using System.Windows.Controls;
using WpfYukiChatClient.Models;

namespace WpfYukiChatClient.Converters;

public class MessageTemplateSelector : DataTemplateSelector {
    public DataTemplate SentMessageTemplate { get; set; } = new();
    public DataTemplate ReceivedMessageTemplate { get; set; } = new();

    public override DataTemplate SelectTemplate(object item, DependencyObject container) => (item as Message) == null ? base.SelectTemplate(item, container) : (item as Message)!.Username != App.Username ? ReceivedMessageTemplate: SentMessageTemplate;
}
