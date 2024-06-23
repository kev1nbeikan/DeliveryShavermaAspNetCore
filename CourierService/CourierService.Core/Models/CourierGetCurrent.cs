namespace CourierService.Core.Models;

public abstract class CourierGetCurrent(
	Guid id,
	string status,
	string basket,
	string comment,
	string storeAddress,
	string clientAddress,
	string clientNumber,
	DateTime deliveryTime)
{
	public Guid Id { get; set; } = id;

	public string Status { get; set; } = status;

	public string Basket { get; set; } = basket;

	public string Comment { get; set; } = comment;

	public string StoreAddress { get; set; } = storeAddress;

	public string ClientAddress { get; set; } = clientAddress;

	public string ClientNumber { get; set; } = clientNumber;

	public DateTime DeliveryTime { get; set; } = deliveryTime;
}