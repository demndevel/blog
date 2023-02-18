using Application.Features.Comments.Queries.GetUnreadComments;

namespace Web.Models;

public class AdminCommentsViewModel
{
    public List<GetUnreadCommentsResultItem> Comments { get; set; } = new();
}