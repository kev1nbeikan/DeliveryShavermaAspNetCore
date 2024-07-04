using System.Text.Json;
using BarsGroupProjectN1.Core.AppSettings;
using BarsGroupProjectN1.Core.Extensions;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarsGroupProjectN1.Core.BackgroundServices;

public abstract class KafkaConsumerService : BackgroundService
{
    protected readonly ILogger<KafkaConsumerService> Logger;
    private IConsumer<Null, string>? _consumer;
    private ConsumerConfig? _consumerConfig;
    private readonly IConfiguration _configuration;
    protected ConsumerOptions ConsumerOptions { get; private set; } = new();
    protected ConsumeResult<Null, string> MessageContext { get; private set; }


    protected KafkaConsumerService(ILogger<KafkaConsumerService> logger, IConfiguration configuration)
    {
        Logger = logger;
        _configuration = configuration;
    }

    protected abstract void OnConfigure(ConsumerOptions consumerOptions);

    protected virtual async Task ProcessMessageAsync(string message)
    {
        Logger.LogInformation($"Processing message: {message}");
    }


    private void Configurate()
    {
        var kafkaOptions = GetKafkaOptions();
        EnsureValidKafkaOptions(kafkaOptions);

        OnConfigure(ConsumerOptions);
        EnsureConsumerOptionsValid();

        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = kafkaOptions!.BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = ConsumerOptions.GroupId,
        };
    }

    private static void EnsureValidKafkaOptions(KafkaOptions? kafkaOptions)
    {
        ArgumentNullException.ThrowIfNull(kafkaOptions,
            "KafkaOptions not found set them is appsettings like \"KafkaOptions\":\n {\"BootstrapServers\": \"localhost:9092\",\n \"GroupId\": \"StoreService\"} \"");
        ArgumentNullException.ThrowIfNull(kafkaOptions.BootstrapServers, "BootstrapServers not found");
        ArgumentNullException.ThrowIfNull(kafkaOptions.GroupId);
    }


    private void SetConsumer()
    {
        _consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build();
        _consumer.Subscribe(ConsumerOptions.Topics);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Configurate();
        SetConsumer();

        ArgumentNullException.ThrowIfNull(_consumer);
        ArgumentNullException.ThrowIfNull(_consumerConfig);

        Logger.LogInformation("Kafka-consumer started");
        Logger.LogInformation(DocString());

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Logger.LogInformation("Kafka-consumer is working");

                MessageContext = await _consumer.ConsumeAsync(stoppingToken);

                await ProcessMessageAsync(MessageContext.Message.Value);
            }
            catch (OperationCanceledException)
            {
                Logger.LogInformation("Kafka-consumer closed");
                break;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error occurred: {ex.Message}");
            }
        }
    }


    private void EnsureConsumerOptionsValid()
    {
        ArgumentNullException.ThrowIfNull(ConsumerOptions);
        ArgumentNullException.ThrowIfNull(ConsumerOptions.GroupId);
        ArgumentNullException.ThrowIfNull(ConsumerOptions.Topics);
    }

    protected virtual KafkaOptions? GetKafkaOptions()
    {
        return _configuration.GetSection("KafkaOptions").Get<KafkaOptions>();
    }

    public virtual string DocString()
    {
        return $"{this} Options: {GetOptionsString()};";
    }

    public string GetOptionsString()
    {
        return JsonSerializer.Serialize(ConsumerOptions);
    }

    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}

public record ConsumerOptions
{
    public List<string>? Topics { get; set; } = null;
    public string? GroupId { get; set; } = null;
}