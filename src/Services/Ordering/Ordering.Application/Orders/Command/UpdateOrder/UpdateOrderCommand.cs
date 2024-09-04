﻿using FluentValidation;
using MediatR;

namespace Ordering.Application.Orders.Command.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("OrderName is required.");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
    }
}