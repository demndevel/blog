using Domain.Entities.Tag;
using Mapster;

namespace Application.Features.Tags.Queries.GetAllTags;

public class GetAllTagsQueryResultMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Tag, GetAllTagsQueryResultItem>()
            .Map(dest => dest.Text, src => src.Text)
            .Map(dest => dest.Id, src => src.Id);
    }
}