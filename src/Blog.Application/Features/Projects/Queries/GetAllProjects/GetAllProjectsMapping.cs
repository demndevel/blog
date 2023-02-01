using Domain.Entities.Project;
using Mapster;

namespace Application.Features.Projects.Queries.GetAllProjects;

public class GetAllProjectsMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, GetAllProjectsQueryResultItem>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.ShortDescription, src => src.ShortDescription)
            .Map(dest => dest.Link, src => src.Link);
    }
}