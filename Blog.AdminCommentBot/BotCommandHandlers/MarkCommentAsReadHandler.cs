using Application.Features.Comments.Commands.MarkCommentAsRead;
using Application.Helpers;
using Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Blog.AdminCommentBot.BotCommandHandlers;

public class MarkCommentAsReadHandler : IBotCommandHandler
{
    private readonly ICommandHandler<MarkCommentAsReadCommand, Unit> _markCommentAsRead;

    public MarkCommentAsReadHandler(ICommandHandler<MarkCommentAsReadCommand, Unit> markCommentAsRead)
    {
        _markCommentAsRead = markCommentAsRead;
    }

    public async Task Handle(ITelegramBotClient client, Update update, CancellationToken ct)
    {
        var id = update.CallbackQuery?.Data?.Split('.')[1];
        var cmd = new MarkCommentAsReadCommand { Id = Guid.Parse(id ?? throw new Exception("An error occured while trying to parse CallbackQuery")) };
        await _markCommentAsRead.Handle(cmd, ct);
    }
}