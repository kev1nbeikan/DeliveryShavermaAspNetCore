namespace StoreService.Core.Abstractions;

public interface ICookingTimerService
{
    TimeSpan GetCookingTime(Guid storeId, List<ProductQuantity> products);
}