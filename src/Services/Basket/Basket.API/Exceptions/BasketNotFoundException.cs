
namespace Basket.API.Exceptions;

public class BasketNotFoundException(string UserName) : NotFoundException("Basket", UserName)
{
    
}
