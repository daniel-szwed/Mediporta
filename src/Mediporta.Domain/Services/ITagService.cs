namespace Mediporta.Domain.Services;

public interface ITagService
{
    float CalculatePercentageShare(Tag tag, long totalCount);
}

public class TagService : ITagService
{
    public float CalculatePercentageShare(Tag tag, long totalCount)
    {
        return (float)tag.Count / totalCount * 100;
    }
}