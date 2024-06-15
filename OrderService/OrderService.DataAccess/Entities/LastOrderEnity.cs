﻿using Newtonsoft.Json.Linq;

namespace OrderService.DataAccess.Entities;

public class LastOrderEnity
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid CourierId { get; set; }
    public Guid StoreId { get; set; }

    public JObject Basket { get; set; } = [];
    

    public int Status { get; set; }
    public int Price { get; set; }

    //public int Grade { get; set; }

    public string Comment { get; set; } = string.Empty;

    public string Cheque { get; } = string.Empty; // пока не уверен как хранить чек

    public string CourierNumber { get; set; } = string.Empty;
    public string ClientNumber { get; set; } = string.Empty;
    public string ClientAddress { get; set; } = string.Empty;


    public TimeSpan CookingTime { get; set; } = TimeSpan.Zero;

    public TimeSpan DeliveryTime { get; set; } = TimeSpan.Zero;


    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public DateTime CookingDate { get; set; } = DateTime.UtcNow;

    public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;
}