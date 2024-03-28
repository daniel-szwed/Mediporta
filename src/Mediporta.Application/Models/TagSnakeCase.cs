namespace Mediporta.Application.Models;

public class TagSnakeCase
{
    public bool has_synonyms { get; set; }
    public bool is_moderator_only { get; set; }
    public bool is_required { get; set; }
    public long count { get; set; }
    public string name { get; set; }
}

public class TagResponse
{
    public IEnumerable<TagSnakeCase> items { get; set; }
}