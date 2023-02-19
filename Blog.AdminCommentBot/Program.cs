using Application;
using Blog.AdminCommentBot;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

var userId = ParseUserId();
var botClient = new TelegramBotClient(ParseBotToken());
Console.WriteLine("Connection string:");
var connectionString = Console.ReadLine();

using CancellationTokenSource cts = new ();

ReceiverOptions receiverOptions = new ()
{
    AllowedUpdates = Array.Empty<UpdateType>()
};

var services = new ServiceCollection()
    .AddApplication()
    .AddPersistence(connectionString ?? throw new Exception("Connection string mustn't be null"))
    .AddSingleton<CommandDispatcher>();
var serviceProvider = services.BuildServiceProvider();

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken ct)
{
    await serviceProvider.GetService<CommandDispatcher>()!.Dispatch(client, update, ct, userId);
}

Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken ct)
{
    var errorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(errorMessage);
    return Task.CompletedTask;
}

int ParseUserId()
{
    int.TryParse(args[0], out var id);
    return id;
}

string ParseBotToken() =>
    args[1] ?? throw new Exception("token mustn't be empty");
