using MediatR;
using Mediporta.Domain;

namespace Mediporta.Application.Commands;

public class InsertTagsCommand : IRequest
{
    public IEnumerable<Tag> Tags { get; }

    public InsertTagsCommand(IEnumerable<Tag> tags)
    {
        Tags = tags;
    }
}