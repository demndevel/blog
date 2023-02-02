using Domain.Entities.Comment;
using Mapster;

namespace Application.Features.Comments.Queries.GetCommentByPostId;

public class GetCommentByPostIdMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Comment, GetCommentByPostIdQueryResultItem>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.DateCreated, src => src.DateCreated)
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.IsAdmin, src => src.IsAdmin);
    }
}