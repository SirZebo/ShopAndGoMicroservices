using BuildingBlocks.Messaging.Events;
using Tracking.API.Dtos;
using MassTransit;
using Tracking.API.Services;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public record DeleteAllTrackingCommand() : ICommand<DeleteAllTrackingResult>;

public record DeleteAllTrackingResult(bool IsDeleted);

internal class DeleteAllTrackingHandler
    (IDocumentSession session)
    : ICommandHandler<DeleteAllTrackingCommand, DeleteAllTrackingResult>
{
    public async Task<DeleteAllTrackingResult> Handle(DeleteAllTrackingCommand command, CancellationToken cancellationToken)
    {
        //Delete all tracking orders from Marten (PostgreSQL)
        session.DeleteWhere<OrderStatus>(x => true);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteAllTrackingResult(true);
    }
}
