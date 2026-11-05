using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfYukiChatClient.Models;

namespace WpfYukiChatClient.ViewModels;

public partial class MainViewModel : ObservableObject {
    [ObservableProperty]
    private ObservableCollection<Message> _messages = [];

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private bool _isConnected = false;

    [ObservableProperty]
    private bool _hasChangeUsername = true;

    private HubConnection? _connection;

    [RelayCommand]
    private async Task GetInputMessage(object parameter) {
        if (parameter is Key key && key == Key.Enter) {
            if (_connection is null || string.IsNullOrWhiteSpace(Message.Trim()) || string.IsNullOrWhiteSpace(Username.Trim()))
                return;

            try {
                await _connection.InvokeAsync("SendMessage", Username, Message);
                Message = string.Empty;
            }
            catch (Exception ex) {
                Messages.Add(new Message { Username = "System", Text = $"Ошибка подключения: {ex.Message}" });
            }

        }
    }

    [RelayCommand]
    private async Task ConnectAsync() {
        Debug.WriteLine($"{Username} clicked");
        if (!IsConnected) {
            if (string.IsNullOrWhiteSpace(Username.Trim()))
                return;

            App.Username = Username;

            _connection = new HubConnectionBuilder()
                .WithUrl("http://yuki.riwvv.ru/chat")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message) => {
                Application.Current.Dispatcher.Invoke(() => {
                    Messages.Add(new Message { Username = user, Text = message.Trim() });
                });
            });

            _connection.Closed += _ => {
                IsConnected = false;
                HasChangeUsername = true;
                return Task.CompletedTask;
            };

            try {
                await _connection.StartAsync();
                IsConnected = true;
                HasChangeUsername = false;
                Messages.Add(new Message { Username = "System", Text = $"{Username} подключился" });
            }
            catch (Exception ex) {
                Messages.Add(new Message { Username = "System", Text = $"Ошибка подключения: {ex.Message}" });
            }
        }
        else {
            if (_connection is null)
                return;

            await _connection.StopAsync();
            await _connection.DisposeAsync();

            _connection = null;
            IsConnected = false;

            Messages.Add(new Message { Username = "System", Text= $"{Username} отключился" });
        }
    }
}
