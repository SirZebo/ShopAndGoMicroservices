using BuildingBlocks.Messaging.Events;
using Finance.API.Dtos;
using Finance.API.Exceptions;
using Finance.API.Payments.MakePayment;
using MassTransit;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Finance.API.Payments.CreatePayment;

public record CreatePaymentCommand(PaymentDto Payment)
    : ICommand<CreatePaymentResult>;

public record CreatePaymentResult(Guid Id);

public class CreatePaymentCommandHandler
    (IPublishEndpoint publishEndpoint, IDocumentSession session, ISender sender, HttpClient httpClient)
    : ICommandHandler<CreatePaymentCommand, CreatePaymentResult>
{
    public async Task<CreatePaymentResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            CustomerId = command.Payment.CustomerId,
            OrderId = command.Payment.OrderId,
            TransactionToken = command.Payment.TransactionToken,
            TotalPrice = command.Payment.TotalPrice,
        };

        
        var account = await session.LoadAsync<Account>(command.Payment.CustomerId, cancellationToken);
        if (account == null)
        {
            throw new AccountNotFoundException(command.Payment.CustomerId);
        }

        var outstandingAmount = payment.TotalPrice - account.Balance;
        if (outstandingAmount < 0)
        {
            outstandingAmount = 0;
        }

        payment.OutstandingAmount = outstandingAmount;
        // save to database
        session.Store(payment);
        await session.SaveChangesAsync(cancellationToken);

        //var eventMessage = MapToPaymentCreatedEvent(payment);
        //await publishEndpoint.Publish(eventMessage, cancellationToken);

        var commercePayStartedEvent = new CommercePayStartedEvent
        {
            PaymentId = payment.Id,
            TransactionToken = payment.TransactionToken,
            TransactionAmount = payment.OutstandingAmount
        };
        //await publishEndpoint.Publish(commercePayStartedEvent, cancellationToken);

        await httpClient.PostAsJsonAsync("http://localhost:6066/createPurchase", new RequestJson
        {
            PaymentId = payment.Id,
            TransactionToken = payment.TransactionToken,
            TransactionAmount = (float) payment.OutstandingAmount
        });

        if (payment.OutstandingAmount == 0)
        {
            var makePaymentCommand = new MakePaymentCommand(payment.Id);
            await sender.Send(makePaymentCommand);
        }

        //if (payment.OutstandingAmount != 0)
        //{
        //    var commercePayStartedEvent = new CommercePayStartedEvent
        //    {
        //        PaymentId = payment.Id,
        //        TransactionToken = payment.TransactionToken,
        //        TransactionAmount = payment.OutstandingAmount
        //    };
        //    await publishEndpoint.Publish(commercePayStartedEvent, cancellationToken);
        //} else
        //{
        //    var makePaymentCommand = new MakePaymentCommand(payment.Id);
        //    await sender.Send(makePaymentCommand);
        //}



        // return result
        return new CreatePaymentResult(payment.Id);
    }

    private PaymentCreatedEvent MapToPaymentCreatedEvent(Payment payment)
    {
        return new PaymentCreatedEvent
        {
            PaymentId = payment.Id,
            TransactionToken = payment.TransactionToken,
            TotalPrice = payment.TotalPrice,
        };
    }

    public class RequestJson
    {
        public Guid PaymentId { get; set; }
        public Guid TransactionToken { get; set; }
        public float TransactionAmount { get; set; }
    }

}
