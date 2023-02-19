using Domain.Entities.Comment;
using Mapster;

namespace Application.Features.Comments.Queries.GetUnreadComments;

public class GetUnreadCommentsMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Comment, GetUnreadCommentsResultItem>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.DateCreated, src => src.DateCreated)
            .Map(dest => dest.PostId, src => src.PostId);
    }
}