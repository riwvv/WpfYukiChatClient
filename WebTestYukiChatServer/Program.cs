using WebTestYukiChatServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => {
    x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});
app.MapHub<ChatHub>("/chat");

app.Run();  