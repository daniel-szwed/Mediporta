using LanguageExt.Common;
using MediatR;
using Mediporta.Domain;
using Mediporta.Domain.Repositories;

namespace Mediporta.Application.Queries;

public class GetTagsQuery : IRequest<Result<IEnumerable<Tag>>>
{
    public int Page { get; }
    public int Pagesize { get; }
    public string Sort { get; }
    public string Order { get; }

    public GetTagsQuery(int page, int pagesize, string sort, string order)
    {
        Page = page;
        Pagesize = pagesize;
        Sort = sort;
        Order = order;
    }
}