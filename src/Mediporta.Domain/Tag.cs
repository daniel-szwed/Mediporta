namespace Mediporta.Domain;

public class Tag : EntityBase
{
    public bool HasSynonyms { get; set; }
    public bool IsModeratorOnly { get; set; }
    public bool IsRequired { get; set; }
    public long Count { get; set; }
    public string Name { get; set; }
    public float PercentageShare { get; set; }
}