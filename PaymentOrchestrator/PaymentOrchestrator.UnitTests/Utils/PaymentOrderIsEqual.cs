using Handler.Core.HanlderService;

namespace HandlerService.UnitTests.Utils;

public static class PaymentOrderIsEqual
{
    public static bool IsEqual(TemporyOrder expected, TemporyOrder actual)
    {
        return expected.Id == actual.Id &&
               expected.Basket.Length == actual.Basket.Length &&
               expected.Price == actual.Price &&
               expected.Comment == actual.Comment &&
               expected.ClientAddress == actual.ClientAddress &&
               expected.ClientId == actual.ClientId &&
               expected.StoreId == actual.StoreId;
    }
}