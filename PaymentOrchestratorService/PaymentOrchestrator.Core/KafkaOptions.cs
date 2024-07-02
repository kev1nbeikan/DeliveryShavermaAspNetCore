namespace Handler.Core;

public class KafkaOptions
{
    public string BootstrapServers { get; set; }
    public string GroupId { get; set; }
}