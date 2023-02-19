using Application.Features.Comments.Commands.DeleteComment;
using Application.Helpers;
using Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Blog.AdminCommentBot.BotCommandHandlers;

public class DeleteCommandHandler : IBotCommandHandler
{
    private readonly int _userId;
    private readonly ICommandHandler<DeleteCommentCommand, Unit> _deleteComment;

    public DeleteCommandHandler(int userId, ICommandHandler<DeleteCommentCommand, Unit> deleteComment)
    {
        _userId = userId;
        _deleteComment = deleteComment;
    }

    public async Task Handle(ITelegramBotClient client, Update update, CancellationToken ct)
    {
        var cmd = new DeleteCommentCommand { Id = Guid.Parse(update.Message!.Text!.Split(' ')[1]) };
        await _deleteComment.Handle(cmd, ct);
        
        await client.SendTextMessageAsync(
            chatId: _userId,
            text: $"Deleted {update.Message!.Text!.Split(' ')[1]}",
            cancellationToken: ct);
    }
}