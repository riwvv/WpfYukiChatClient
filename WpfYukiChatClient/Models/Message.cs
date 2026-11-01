namespace WpfYukiChatClient.Models;

public class Message {
    public string Username { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public TimeOnly ReceiptTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);
}
