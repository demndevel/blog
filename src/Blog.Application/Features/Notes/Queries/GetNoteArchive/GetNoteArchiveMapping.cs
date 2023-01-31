using Domain.Entities.Note;
using Mapster;

namespace Application.Features.Notes.Queries.GetNoteArchive;

public class GetNoteArchiveMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Note, GetNoteArchiveQueryResultItem>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Date, src => src.Date);
    }
}