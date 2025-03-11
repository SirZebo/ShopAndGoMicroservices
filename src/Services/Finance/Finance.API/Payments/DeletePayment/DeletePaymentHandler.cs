using BuildingBlocks.Messaging.Events;
using Finance.API.Exceptions;
using MassTransit;

namespace Finance.API.Payments.DeletePayment;

public record DeletePaymentCommand(Guid Id) : ICommand<DeletePaymentResult>;

public record DeletePaymentResult(bool IsSuccess);
public class DeletePaymentCommandValidator : AbstractValidator<DeletePaymentCommand>
{
    public DeletePaymentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Payment Id is required");
    }
}

public class DeletePaymentHandlerCommandHandler
    (IDocumentSession session,
    IPublishEndpoint publishEndpoint)
    : ICommandHandler<DeletePaymentCommand, DeletePaymentResult>
{
    public async Task<DeletePaymentResult> Handle(DeletePaymentCommand command, CancellationToken cancellationToken)
    {
        var payment = await session.LoadAsync<Payment>(command.Id, cancellationToken);

        if (payment == null)
        {
            throw new PaymentNotFoundException(command.Id);
        }

        session.Delete<Payment>(payment.Id);
        await session.SaveChangesAsync();

        var paymentDeletedEvent = new PaymentDeletedEvent {
            PaymentId = payment.OrderId,
        };
        await publishEndpoint.Publish(paymentDeletedEvent, cancellationToken);

        return new DeletePaymentResult(true);
    }
}