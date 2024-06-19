using Newtonsoft.Json.Linq;

namespace OrderService.DataAccess.Entities;

public class CanceledOrderEntity : BaseOrderEntity
{
    public int LastStatus { get; set; }
    public string ReasonOfCanceled { get; set; } = string.Empty;
}