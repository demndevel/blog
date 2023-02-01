namespace Web.Models;

public class SearchNotesByTagsModel
{
    public string IncludedTags { get; set; } = "";
    public string ExcludedTags { get; set; } = "";
}