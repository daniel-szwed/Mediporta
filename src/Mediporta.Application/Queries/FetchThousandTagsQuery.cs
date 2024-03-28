using MediatR;
using Mediporta.Domain;

namespace Mediporta.Application.Queries;

public class FetchThousandTagsQuery : IRequest<IEnumerable<Tag>>
{
    
}