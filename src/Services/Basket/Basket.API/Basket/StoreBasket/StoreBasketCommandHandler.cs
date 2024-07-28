namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string userName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart field is required");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("username is required");
    }
}
public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart shoppingCartForCreate = command.Cart;
        await repository.StoreBasket(shoppingCartForCreate, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }
}
