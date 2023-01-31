using Domain.Entities.Note;
using Mapster;

namespace Application.Features.Notes.Queries.GetNotesByPage;

public class GetNotesByPageQueryMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Note, GetNotesByPageQueryResultItem>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Date, src => src.Date)
            .Map(dest => dest.ShortDescription, src => src.ShortDescription)
            .Map(dest => dest.Tags, src => src.Tags)
            .Map(dest => dest.Text, src => src.Text);
    }
}