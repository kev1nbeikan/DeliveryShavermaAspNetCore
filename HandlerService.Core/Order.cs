namespace Handler.Core;

public class Order
{
    public Guid Id { get; set; }
    Guid UserId { get; set; }
    public Product[] Products { get; set; }
    long Price { get; set; }
    string PaymentRequestAddress { get; set; }
    string PaymentRequestComment { get; set; }

    private Order(Guid id, Guid userId, Product[] products, long price, string paymentRequestAddress,
        string paymentRequestComment)
    {
        this.Id = id;
        this.UserId = userId;
        this.Products = products;
        this.Price = price;
        this.PaymentRequestAddress = paymentRequestAddress;
        this.PaymentRequestComment = paymentRequestComment;
    }

    public static (Order? order, string? error) Create(Guid id, Guid userId, Product[] products, long price,
        string paymentRequestAddress, string paymentRequestComment)
    {
        string error = null;

        return (new Order(id, userId, products, price, paymentRequestAddress, paymentRequestComment), error);
    }
}