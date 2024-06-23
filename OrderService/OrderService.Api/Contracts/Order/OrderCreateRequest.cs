﻿using Newtonsoft.Json.Linq;
using OrderService.Domain.Models.Code;

namespace OrderService.Api.Contracts.Order;

public record OrderCreateRequest(
    Guid Id,
    Guid ClientId,
    Guid CourierId,
    Guid StoreId,
    JObject Basket,
    int Price,
    string Comment,
    string StoreAddress,
    string ClientAddress,
    string CourierNumber,
    string ClientNumber,
    TimeSpan CookingTime,
    TimeSpan DeliveryTime,
    string Cheque);