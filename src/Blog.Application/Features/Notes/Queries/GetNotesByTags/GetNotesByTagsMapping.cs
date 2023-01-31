using Domain.Entities.Note;
using Mapster;

namespace Application.Features.Notes.Queries.GetNotesByTags;

public class GetNotesByTagsMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Note, GetNotesByTagsQueryResultItem>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.ShortDescription, src => src.ShortDescription)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.Tags, src => src.Tags);
    }
}