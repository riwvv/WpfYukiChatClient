using Microsoft.Xaml.Behaviors;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace WpfYukiChatClient.Behaviors;

public class AutoScrollToBottomBehavior : Behavior<ListBox> {
    protected override void OnAttached() {
        base.OnAttached();
        if (AssociatedObject.ItemsSource is INotifyCollectionChanged collection)
            collection.CollectionChanged += OnCollectionChanged;
    }

    protected override void OnDetaching() {
        base.OnDetaching();
        if (AssociatedObject.ItemsSource is INotifyCollectionChanged collection)
            collection.CollectionChanged -= OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        if (e.Action == NotifyCollectionChangedAction.Add && AssociatedObject.Items.Count > 0) {
            var lastItem = AssociatedObject.Items[^1];
            AssociatedObject.ScrollIntoView(lastItem);
        }
    }
}
