using FluentValidation;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(OrderDto Order)
    : ICommand<UpdateOrderStatusResult>;

public record UpdateOrderStatusResult(bool IsSuccess);

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(x => x.Order.Id)
            .NotEmpty().WithMessage("OrderId is required");
        RuleFor(x => x.Order.Status)
            .NotEmpty().WithMessage("OrderStatus is required");
    }
}
