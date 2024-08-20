namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus OrderStatus { get; private set; } = default!;

    public decimal TotalPrice => OrderItems.Sum(item => item.Price);

    public static Order Create(OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress,
        Address billingAddress, Payment payment)
    {
        ArgumentNullException.ThrowIfNull(customerId);
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentNullException.ThrowIfNull(shippingAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(payment);

        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            OrderStatus = OrderStatus.Pending
        };
        
        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }
    
    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
    {
        ArgumentNullException.ThrowIfNull(orderName);
        ArgumentNullException.ThrowIfNull(shippingAddress);
        ArgumentNullException.ThrowIfNull(billingAddress);
        ArgumentNullException.ThrowIfNull(payment);

        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        OrderStatus = status;
        
        AddDomainEvent(new OrderUpdatedEvent(this));
    }
    
    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        // var existingOrderForProduct = _orderItems.FirstOrDefault(o => o.ProductId == productId);
        // if (existingOrderForProduct != null)
        // {
        //     existingOrderForProduct.AddQuantity(quantity);
        // }
        // else
        // {
            _orderItems.Add(new OrderItem(Id, productId, quantity, price));
        // }
    }
    
    public void Remove(ProductId productId)
    {
        var existingOrderForProduct = _orderItems.FirstOrDefault(o => o.ProductId == productId);
        if (existingOrderForProduct != null)
        {
            _orderItems.Remove(existingOrderForProduct);
        }
    }
}