using Handler.Core.Common;
using Handler.Core.Payment;

namespace Handler.Core.HanlderService
{
    /// <summary>
    /// Represents a temporary order that inherits certain fields from the Order class to store it in the database until payment is confirmed.
    /// </summary>
    public record PaymentOrder
    {
        public const int MAX_NUMBER_LENGHT = 10;
        public const int MAX_ADDRESS_LENGHT = 250;
        public const int MAX_COMMENT_LENGHT = 250;

        public PaymentOrder(Guid id,
            Product[] produtsList,
            List<(Product product, int amount, int price)> productAndQuantity,
            int price,
            string comment,
            string clientAddress,
            Guid clientId,
            string clientNumber)
        {
            Id = id;
            ProdutsList = produtsList;
            Price = price;
            Comment = comment;
            ClientAddress = clientAddress;
            ClientId = clientId;
            ClientNumber = clientNumber;
            Bucket = productAndQuantity;
        }


        public Guid Id { get; }


        public Guid ClientId { get; }


        public string ClientNumber { get; }


        public Product[] ProdutsList { get; } = [];

        public List<(Product product, int amount, int price)> Bucket { get; } = [];


        public int Price { get; }


        public string Comment { get; } = string.Empty;


        public string ClientAddress { get; } = string.Empty;


        public static (PaymentOrder? Order, string? Error) Create(Guid id,
            Product[] basket,
            List<(Product product, int amount, int price)> productQuantity,
            int price,
            string comment,
            string clientAddress,
            Guid clientId,
            string clientNumber)
        {
            string errorString = string.Empty;

            if (string.IsNullOrEmpty(clientAddress) || clientAddress.Length > MAX_ADDRESS_LENGHT)
                errorString = "Error in client address, the value is empty or exceeds the maximum value";

            if (comment.Length > MAX_COMMENT_LENGHT)
                errorString = "Error in the comment, the value is empty or exceeds the maximum value";


            if (basket is null)
                errorString = "Error in the basket, the value is empty";

            if (clientId == Guid.Empty)
                errorString = "Error in clientId, value is empty";

            if (string.IsNullOrEmpty(clientNumber) || clientNumber.Length > MAX_NUMBER_LENGHT)
                errorString = "Error in clientNumber, is empty or exceeds the maximum value " + MAX_NUMBER_LENGHT;

            if (price < 0)
                errorString = "Error in price, negative value for price";

            if (productQuantity.Count != basket.Length)
                errorString = "Incorrect productQuantity or basket";

            var task = new PaymentOrder(id,
                basket!,
                productQuantity,
                price,
                comment,
                clientAddress,
                clientId,
                clientNumber);

            return (task, errorString);
        }
    }
}