using System.ComponentModel;
using LanguageExt.Common;
using MediatR;
using Mediporta.Application.Queries;
using Mediporta.Domain;
using Mediporta.Domain.Repositories;

namespace Mediporta.Application.Handlers;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, Result<IEnumerable<Tag>>>
{
    private readonly IRepository<Tag> _tagRepository;

    public GetTagsQueryHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    
    public async Task<Result<IEnumerable<Tag>>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var sortByProperty = TypeDescriptor
            .GetProperties(typeof(Tag))
            .Find(request.Sort, true);

        if (sortByProperty is null)
        {
            return BadResult($"Tag doesn't contain property '{request.Sort}'");
        }

        var allowedOrders = new[] { "asc", "desc" };

        if (!allowedOrders.Contains(request.Order))
        {
            return BadResult($"Order can be 'asc' or 'desc' only.");
        }

        var result = _tagRepository.GetMany();
        
        if (request.Order == "asc")
        {
            result = result.OrderBy(tag => sortByProperty.GetValue(tag));
        }
        else
        {
            result = result.OrderByDescending(tag => sortByProperty.GetValue(tag));
        }
            
        return result
                .Skip((request.Page - 1) * request.Pagesize)
                .Take(request.Pagesize)
                .ToList();
    }

    private static Result<IEnumerable<Tag>> BadResult(string message)
    {
        var exception = new Exception(message);

        return new Result<IEnumerable<Tag>>(exception);
    }
}