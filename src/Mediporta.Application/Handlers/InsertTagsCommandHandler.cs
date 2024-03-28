using MediatR;
using Mediporta.Application.Commands;
using Mediporta.Domain;
using Mediporta.Domain.Repositories;

namespace Mediporta.Application.Handlers;

public class InsertTagsCommandHandler : IRequestHandler<InsertTagsCommand>
{
    private readonly IRepository<Tag> _tagRepository;

    public InsertTagsCommandHandler(IRepository<Tag> tagRepository)
    {
        _tagRepository = tagRepository;
    }
    
    public Task Handle(InsertTagsCommand request, CancellationToken cancellationToken)
    {
        _tagRepository.AddRange(request.Tags);
        
        return Task.CompletedTask;
    }
}