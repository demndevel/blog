using Blog.Models;

namespace Blog;

public static class SearchByTag
{
    public static bool Search(Note note, int tag)
    {
        foreach (var t in note.Tags)
            if (t == tag)
                return true;
        return false;
    }
}