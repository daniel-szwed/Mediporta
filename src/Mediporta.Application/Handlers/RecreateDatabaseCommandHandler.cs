using MediatR;
using Mediporta.Application.Commands;
using Mediporta.Application.Queries;

namespace Mediporta.Application.Handlers;

public class RecreateDatabaseCommandHandler : IRequestHandler<RecreateDatabaseCommand>
{
    private readonly IMediator _mediator;

    public RecreateDatabaseCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Handle(RecreateDatabaseCommand request, CancellationToken cancellationToken)
    {
        var truncateTagsRequest = new TruncateTagsTableCommand();
        await _mediator.Send(truncateTagsRequest);

        var fetchTousandTagsRequest = new FetchThousandTagsQuery();
        var tags = await _mediator.Send(fetchTousandTagsRequest);

        var insertTagsCommand = new InsertTagsCommand(tags);
        await _mediator.Send(insertTagsCommand);
    }
}