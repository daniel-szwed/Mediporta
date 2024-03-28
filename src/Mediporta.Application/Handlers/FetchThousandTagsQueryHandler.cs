using System.Collections.Concurrent;
using MediatR;
using Mediporta.Application.Models;
using Mediporta.Application.Queries;
using Mediporta.Domain;
using Mediporta.Domain.Services;
using Microsoft.Extensions.Options;

namespace Mediporta.Application.Handlers;

public class FetchThousandTagsQueryHandler : IRequestHandler<FetchThousandTagsQuery, IEnumerable<Tag>>
{
    private readonly IApiService _apiService;
    private readonly ITagService _tagService;
    private readonly TagSource _tagSource;

    public FetchThousandTagsQueryHandler(
        IApiService apiService, 
        ITagService tagService,
        IOptions<TagSource> stackOverflowOptions)
    {
        _apiService = apiService;
        _tagService = tagService;
        _tagSource = stackOverflowOptions.Value;
    }
    
    public async Task<IEnumerable<Tag>> Handle(FetchThousandTagsQuery request, CancellationToken cancellationToken)
    {
        var result = new ConcurrentBag<Tag>();
        var url = _tagSource.Url;
        var pages = new List<int>()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        };

        await Parallel.ForEachAsync(
            pages, 
            cancellationToken, 
            async (page, cancellationToken) =>
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "order", "desc" },
                { "sort", "popular" },
                { "site", "stackoverflow" },
                { "pagesize", "100"},
                { "page", $"{page}" }
            };
            
            var tagsFromSo = await _apiService.GetAsync<TagResponse>(url, queryParams);
            var tags = tagsFromSo.items.Select(tag => 
                new Tag
                {
                    Name = tag.name,
                    Count = tag.count,
                    HasSynonyms = tag.has_synonyms,
                    IsRequired = tag.is_required,
                    IsModeratorOnly = tag.is_moderator_only
                });
            foreach (var tag in tags)
            {
                result.Add(tag);
            }
        });

        var totalCount = result.Sum(tag => tag.Count);

        foreach (var tag in result)
        {
            tag.PercentageShare = _tagService.CalculatePercentageShare(tag, totalCount);
        }
        
        return result;
    }
}