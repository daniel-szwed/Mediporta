namespace Mediporta.Domain.Services;

public class TagServiceTests
{
    private readonly ITagService _sut;
    
    public TagServiceTests()
    {
        _sut = new TagService();
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void GetValueForKey_CustomMapping_ShiftedKey(long tagCount, long totalCount, float expected)
    {
        // Arrange
        var tag = new Tag()
        {
            Count = tagCount
        };

        // Act
        var percentageShare = _sut.CalculatePercentageShare(tag, totalCount);
        
        // Assert
        Assert.Equal(expected, percentageShare);
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[] {5, 25, 20f};
        yield return new object[] {1, 20, 5f};
        yield return new object[] {14, 28, 50f};
        yield return new object[] {18, 64, 28.125};
    }
}