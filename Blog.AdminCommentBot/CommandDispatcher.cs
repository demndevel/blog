using Application.Features.Comments.Commands.DeleteComment;
using Application.Features.Comments.Commands.MarkCommentAsRead;
using Application.Features.Comments.Queries.GetUnreadComments;
using Application.Helpers;
using Application.Interfaces;
using Blog.AdminCommentBot.BotCommandHandlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Blog.AdminCommentBot;

public class CommandDispatcher
{
    private readonly IQueryHandler<GetUnreadCommentsQuery, GetUnreadCommentsResult> _getUnreadComments;
    private readonly ICommandHandler<MarkCommentAsReadCommand, Unit> _markCommentAsRead;
    private readonly ICommandHandler<DeleteCommentCommand, Unit> _deleteComment;
    
    public CommandDispatcher(IQueryHandler<GetUnreadCommentsQuery, GetUnreadCommentsResult> getUnreadComments, ICommandHandler<DeleteCommentCommand, Unit> deleteComment, ICommandHandler<MarkCommentAsReadCommand, Unit> markCommentAsRead)
    {
        _getUnreadComments = getUnreadComments;
        _deleteComment = deleteComment;
        _markCommentAsRead = markCommentAsRead;
    }

    public async Task Dispatch(ITelegramBotClient client, Update update, CancellationToken ct, int userId)
    {
        IBotCommandHandler handler = update.Type switch
        {
            UpdateType.Message when update.Message?.Text == "/get" => new GetCommandHandler(userId, _getUnreadComments),
            UpdateType.Message when update.Message is not null && update.Message.Text!.StartsWith("/delete") =>
                new DeleteCommandHandler(userId, _deleteComment),
            UpdateType.CallbackQuery when update.CallbackQuery?.Message?.Text != null => new MarkCommentAsReadHandler(_markCommentAsRead),
            _ => null!
        };

        try
        {
            if (!ReferenceEquals(handler, null))
                await handler.Handle(client, update, ct);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}