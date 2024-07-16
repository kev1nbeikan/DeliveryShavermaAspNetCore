using BarsGroupProjectN1.Core.Models.Order;
using OrderService.Domain.Models;

namespace OrderService.UnitTests.Data;

public static class TestCurrentOrderData
{
    public static CurrentOrder Correct = CurrentOrder.Create(
        Guid.NewGuid(), // id
        Guid.NewGuid(), // clientId
        Guid.NewGuid(), // courierId
        Guid.NewGuid(), // storeId
        new List<BasketItem>
        {
            new BasketItem
            {
                ProductId = Guid.NewGuid(), Name = "Product name", Amount =
                    1,
                Price = 10
            }
        }, // basket
        100, // price
        "Some comment", // comment
        "Store address", // storeAddress
        "Client address", // clientAddress
        "89874999999", // courierNumber
        "89874999999", // clientNumber
        TimeSpan.FromMinutes(30), // cookingTime
        TimeSpan.FromMinutes(45), // deliveryTime
        DateTime.UtcNow, // orderDate
        DateTime.UtcNow, // cookingDate
        DateTime.UtcNow, // deliveryDate
        "Some cheque", // cheque
        StatusCode.Cooking // status
    );


    // public static CurrentOrder EmptyOrderData = CurrentOrder.Create(
    //     Guid.NewGuid(),
    //     Guid.Empty,
    //     Guid.Empty,
    //     Guid.Empty,
    //     new List<BasketItem>(),
    //     0,
    //     "",
    //     "",
    //     "",
    //     "",
    //     "",
    //     TimeSpan.Zero,
    //     TimeSpan.Zero,
    //     DateTime.MinValue,
    //     DateTime.MinValue,
    //     DateTime.MinValue,
    //     "",
    //     StatusCode.Cooking
    // );
    //
    // public static CurrentOrder? NullOrderData = null;
}