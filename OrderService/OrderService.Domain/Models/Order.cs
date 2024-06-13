namespace OrderService.Domain.Models
{
    public class Order
    {
        public enum StatusCode
        {
            Cancelled = 0,
            Active = 1,
            Cooking = 2,
            WaitingCourier = 3,
            Delivering = 4, 
            WaitingClinet = 5,
            Accepted = 6
        }

        int Active = (int)Status.Active;
        public const int MAX_NUMBER_LENGHT = 10;
        public const int MAX_ADDRESS_LENGHT = 250;
        public const int ACTIVE = 1;
        public const int DELETED = 2;

        private Order(Guid id,
                     int status,
                     int price,
                     string clientAddress,
                     string courierNumber,
                     string clientNumber,
                     TimeSpan cookingTime,
                     TimeSpan deliveryTime,
                     DateTime orderDate,
                     DateTime cookingDate,
                     DateTime deliveryDate)
        {
            Id = id;
            //Status = status;
            Price = price;
            ClientAddress = clientAddress;
            CourierNumber = courierNumber;
            ClientNumber = clientNumber;
            CookingTime = cookingTime;
            DeliveryTime = deliveryTime;
            OrderDate = orderDate;
            CookingDate = cookingDate;
            DeliveryDate = deliveryDate;
        }

        public Guid Id { get; }

        
        public List<Order> Orders { get; } = [];


        public int Status { get; } = 1;

        public int Price { get; }

        
        public string Comment { get; } = string.Empty;

        public string Cehque { get; } = string.Empty;


        public string ClientAddress { get; } = string.Empty;

        public string CourierNumber { get; } = string.Empty;

        public string ClientNumber { get; } = string.Empty;



        public TimeSpan CookingTime { get; } = TimeSpan.Zero;

        public TimeSpan DeliveryTime { get; } = TimeSpan.Zero;


        public DateTime OrderDate { get; } = DateTime.UtcNow;
        
        public DateTime CookingDate { get; } = DateTime.UtcNow;

        public DateTime DeliveryDate { get; } = DateTime.UtcNow;



        public static (Order Order, string Error) Create(Guid id,
                                                         int status,
                                                         int price,
                                                         string clientAddress,
                                                         string courierNumber,
                                                         string clientNumber,
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

            if (price < 0)
                errorString = "Error in price, negative value for price";

            var task = new Order(id,
                                 status,
                                 price,
                                 clientAddress,
                                 courierNumber,
                                 clientNumber,
                                 cookingTime,
                                 deliveryTime,
                                 orderDate,
                                 cookingDate,
                                 deliveryDate);

            return (task, errorString);
        }
    }
}
