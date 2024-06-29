using Handler.Core.HanlderService;

namespace HandlerService.UnitTests.Utils;

public static class PaymentOrderIsEqual
{
    public static bool IsEqual(PaymentOrder expected, PaymentOrder actual)
    {
        return expected.Id == actual.Id &&
               expected.ProdutsList.Length == actual.ProdutsList.Length &&
               expected.Price == actual.Price &&
               expected.Comment == actual.Comment &&
               expected.ClientAddress == actual.ClientAddress &&
               expected.ClientId == actual.ClientId &&
               expected.ClientNumber == actual.ClientNumber;
    }
}