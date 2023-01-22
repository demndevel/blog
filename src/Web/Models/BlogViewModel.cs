using Domain.Entities;

namespace Web.Models;

public class BlogViewModel
{
    public List<Note> Notes { get; set; } = null!;
    public Tag[] Tags { get; set; } = null!;
}