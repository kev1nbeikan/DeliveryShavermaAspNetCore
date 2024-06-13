namespace OrderService.Domain.Models
{
    public class Order
    {
        public const int MAX_NUMBER_LENGHT = 10;
        public const int MAX_ADDRESS_LENGHT = 250;
        public const int MAX_COMMENT_LENGHT = 250;

        public Order(Guid id,
                     StatusCode status,
                     Dictionary<Guid, int> basket,
                     int price,
                     string comment,
                     string cheque,
                     string clientAddress,
                     string courierNumber,
                     string clientNumber,
                     Guid clientId,
                     Guid courierId,
                     Guid storeId,
                     TimeSpan cookingTime,
                     TimeSpan deliveryTime,
                     DateTime orderDate,
                     DateTime cookingDate,
                     DateTime deliveryDate)
        {
            Id = id;
            Status = status;
            Basket = basket;
            Price = price;
            Comment = comment;
            Cheque = cheque;
            ClientAddress = clientAddress;
            CourierNumber = courierNumber;
            ClientNumber = clientNumber;
            ClientId = clientId;
            CourierId = courierId;
            StoreId = storeId;
            CookingTime = cookingTime;
            DeliveryTime = deliveryTime;
            OrderDate = orderDate;
            CookingDate = cookingDate;
            DeliveryDate = deliveryDate;
        }


        public Guid Id { get; }


        public Guid ClientId { get; }

        public Guid CourierId { get; }

        public Guid StoreId { get; }


        public Dictionary<Guid, int> Basket { get; } = []; // проверка значений словаря


        public StatusCode Status { get; } = StatusCode.Active;


        public int Price { get; }


        public string Comment { get; } = string.Empty;

        public string Cheque { get; } = string.Empty; // пока не уверен как хранить чек


        public string ClientAddress { get; } = string.Empty;

        public string CourierNumber { get; } = string.Empty;

        public string ClientNumber { get; } = string.Empty;

       
        public TimeSpan CookingTime { get; } = TimeSpan.Zero;

        public TimeSpan DeliveryTime { get; } = TimeSpan.Zero;


        public DateTime OrderDate { get; } = DateTime.UtcNow;

        public DateTime CookingDate { get; } = DateTime.UtcNow;

        public DateTime DeliveryDate { get; } = DateTime.UtcNow;



        public static (Order Order, string Error) Create(Guid id,
                                                         StatusCode status,
                                                         Dictionary<Guid, int> basket,
                                                         int price,
                                                         string comment,
                                                         string cheque,
                                                         string clientAddress,
                                                         string courierNumber,
                                                         string clientNumber,
                                                         Guid clientId,
                                                         Guid courierId,
                                                         Guid storeId,
                                                         TimeSpan cookingTime,
                                                         TimeSpan deliveryTime,
                                                         DateTime orderDate,
                                                         DateTime cookingDate,
                                                         DateTime deliveryDate)
        {
            string errorString = string.Empty;

            if (string.IsNullOrEmpty(clientAddress) || clientAddress.Length > MAX_ADDRESS_LENGHT)
                errorString = "Error in client address, the value is empty or exceeds the maximum value";

            if (string.IsNullOrEmpty(clientNumber) || clientNumber.Length > MAX_NUMBER_LENGHT)
                errorString = "Error in client number, the value is empty or exceeds the maximum value";

            if (string.IsNullOrEmpty(courierNumber) || courierNumber.Length > MAX_NUMBER_LENGHT)
                errorString = "Error in the courier number, the value is empty or exceeds the maximum value";

            if (string.IsNullOrEmpty(comment) || comment.Length > MAX_COMMENT_LENGHT)
                errorString = "Error in the comment, the value is empty or exceeds the maximum value";

            if (string.IsNullOrEmpty(cheque))
                errorString = "Error in the cheque, the value is empty";

            if (basket is null)
                errorString = "Error in the basket, the value is empty";

            if(clientId == Guid.Empty)
                errorString = "Error in clientId, value is empty";

            if (courierId == Guid.Empty)
                errorString = "Error in courierId, value is empty";

            if (storeId == Guid.Empty)
                errorString = "Error in storeId, value is empty";

            if (price < 0)
                errorString = "Error in price, negative value for price";

            if (cookingTime < TimeSpan.Zero)
                errorString = "Error in cookingTime, negative value for price";

            if (deliveryTime < TimeSpan.Zero)
                errorString = "Error in deliveryTime, negative value for price";


            var task = new Order(id,
                                 status,
                                 basket,
                                 price,
                                 comment,
                                 cheque,
                                 clientAddress,
                                 courierNumber,
                                 clientNumber,
                                 clientId,
                                 courierId,
                                 storeId,
                                 cookingTime,
                                 deliveryTime,
                                 orderDate,
                                 cookingDate,
                                 deliveryDate);

            return (task, errorString);
        }
    }
}
