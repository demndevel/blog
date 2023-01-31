using Domain.Entities.Note;
using Mapster;

namespace Application.Features.Notes.Queries.GetAllNotes;

public class GetAllNotesMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Note, GetAllNotesQueryResultItem>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title);
    }
}