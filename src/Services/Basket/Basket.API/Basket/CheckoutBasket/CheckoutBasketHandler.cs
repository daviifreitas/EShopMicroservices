using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{

    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithName("BasketCheckoutDto required!");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithName("UserName required!");
        }
    }

    public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(request.BasketCheckoutDto.UserName, cancellationToken);

            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }

            var checkoutEvent = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();

            checkoutEvent.TotalPrice = basket.TotalPrice;
            await publishEndpoint.Publish(checkoutEvent, cancellationToken);

            await repository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
