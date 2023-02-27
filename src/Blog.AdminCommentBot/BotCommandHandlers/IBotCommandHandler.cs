using Telegram.Bot;
using Telegram.Bot.Types;

namespace Blog.AdminCommentBot.BotCommandHandlers;

public interface IBotCommandHandler
{
    Task Handle(ITelegramBotClient client, Update update, CancellationToken ct);
}