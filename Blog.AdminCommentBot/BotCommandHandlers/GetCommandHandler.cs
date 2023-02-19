using Application.Features.Comments.Queries.GetUnreadComments;
using Application.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Blog.AdminCommentBot.BotCommandHandlers;

public class GetCommandHandler : IBotCommandHandler
{
    private readonly int _userId;
    private readonly IQueryHandler<GetUnreadCommentsQuery, GetUnreadCommentsResult> _getUnreadComments;

    public GetCommandHandler(int userId, IQueryHandler<GetUnreadCommentsQuery, GetUnreadCommentsResult> getUnreadComments)
    {
        _userId = userId;
        _getUnreadComments = getUnreadComments;
    }

    public async Task Handle(ITelegramBotClient client, Update update, CancellationToken ct)
    {
        var comment = await GetOldestUnreadComment(ct);
        
        var text = $"""
Id: `{comment.Id}`
Post link: https://demns.space/note/{comment.PostId}
Name: {comment.Name}
Text: {comment.Text}
""";
        InlineKeyboardMarkup inlineKeyboard = new(new[]
        {
            new [] { InlineKeyboardButton.WithCallbackData(text: "Mark as read", callbackData: "read."+comment.Id) }
        });
        
        await client.SendTextMessageAsync(
            chatId: _userId,
            text: text,
            replyMarkup: inlineKeyboard,
            // parseMode: ParseMode.MarkdownV2,
            cancellationToken: ct);
    }

    private async Task<GetUnreadCommentsResultItem> GetOldestUnreadComment(CancellationToken ct)
    {
        var result = await _getUnreadComments.Handle(new(), ct);
        var oldestUnreadComment = result.Comments.MinBy(c => c.DateCreated);

        return oldestUnreadComment ?? new GetUnreadCommentsResultItem
        {
            Id = Guid.NewGuid(),
            Name = "None",
            Text = "Error occured",
            PostId = 0,
            DateCreated = DateTime.Now
        };
    }
}