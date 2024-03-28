using LanguageExt.Common;
using MediatR;
using Mediporta.Application.Commands;
using Mediporta.Domain;
using Mediporta.Domain.Repositories;

namespace Mediporta.Application.Handlers;

public class TruncateTagsTableCommandHandler : IRequestHandler<TruncateTagsTableCommand>
{
    private readonly IRepository<Tag> _tagRepository;

    public TruncateTagsTableCommandHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    
    public Task Handle(TruncateTagsTableCommand request, CancellationToken cancellationToken)
    {
        _tagRepository.ExecuteSqlRaw("DELETE FROM Tags;");

        return Task.CompletedTask;
    }
}