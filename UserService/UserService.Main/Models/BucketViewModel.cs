using UserService.Main.Contracts;

namespace UserService.Main.Models;

public class BucketViewModel
{
    public List<BucketItem> Products { get; set; } = [];
    public List<string> Addresses { get; set; } = [];
    public string SelectedAddress { get; set; } = "";
    public string DefaultComment { get; set; } = "";

    public string PhoneNumber { get; set; } = "";
    public string DisplayStyleOfNewAddressInput => Addresses.Count == 0 ? "display: block" : "display: none";
}