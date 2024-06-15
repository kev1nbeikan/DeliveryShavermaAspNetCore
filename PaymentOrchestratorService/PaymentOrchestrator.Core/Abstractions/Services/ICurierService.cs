namespace HandlerService.Controllers;

public interface ICurierService
{
    Task<(Curier?, TimeSpan deliveryTime)> GetCurier(string clientAddress);
}