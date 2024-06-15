namespace Handler.Core.HanlderService
{
    /// <summary>
    /// Represents a temporary order that inherits certain fields from the Order class to store it in the database until payment is confirmed.
    /// </summary>
    public class TemporyOrder
    {
        public const int MAX_NUMBER_LENGHT = 10;
        public const int MAX_ADDRESS_LENGHT = 250;
        public const int MAX_COMMENT_LENGHT = 250;

        public TemporyOrder(Guid id,
            Product[] basket,
            int price,
            string comment,
            string clientAddress,
            Guid clientId,
            Guid storeId)
        {
            Id = id;
            Basket = basket;
            Price = price;
            Comment = comment;
            ClientAddress = clientAddress;
            ClientId = clientId;
            StoreId = storeId;
        }


        public Guid Id { get; }


        public Guid ClientId { get; }


        public Guid StoreId { get; }


        public Product[] Basket { get; } = []; // проверка значений словаря


        public int Price { get; }


        public string Comment { get; } = string.Empty;


        public string ClientAddress { get; } = string.Empty;


        public static (TemporyOrder? Order, string? Error) Create(Guid id,
            Product[] basket,
            int price,
            string comment,
            string clientAddress,
            Guid clientId,
            Guid storeId)
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


            if (storeId == Guid.Empty)
                errorString = "Error in storeId, value is empty";

            if (price < 0)
                errorString = "Error in price, negative value for price";


            var task = new TemporyOrder(id,
                basket!,
                price,
                comment,
                clientAddress,
                clientId,
                storeId);

            return (task, errorString);
        }
    }
}