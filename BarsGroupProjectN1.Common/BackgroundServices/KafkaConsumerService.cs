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
    protected ConsumerOptions Options { get; private set; }
    protected ConsumeResult<Null, string> Context { get; private set; }


    protected KafkaConsumerService(ILogger<KafkaConsumerService> logger, IConfiguration configuration)
    {
        Logger = logger;
        _configuration = configuration;
    }

    protected virtual void Configurate()
    {
        var kafkaOptions = _configuration.GetSection("KafkaOptions").Get<KafkaOptions>();
        ArgumentNullException.ThrowIfNull(kafkaOptions, "KafkaOptions not found");
        OnConfigure(Options);
        EnsureConsumerOptionsValid();

        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = kafkaOptions.BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = Options.GroupId,
        };
    }

    protected abstract void OnConfigure(ConsumerOptions consumerOptions);

    private void SetConsumer()
    {
        _consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build();
        _consumer.Subscribe(Options.Topics);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Configurate();
        SetConsumer();

        ArgumentNullException.ThrowIfNull(_consumer);
        ArgumentNullException.ThrowIfNull(_consumerConfig);

        Logger.LogInformation("Kafka-consumer started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Logger.LogInformation("Kafka-consumer is working");

                Context = await _consumer.ConsumeAsync(stoppingToken);

                await ProcessMessageAsync(Context.Message.Value);
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

    protected virtual async Task ProcessMessageAsync(string message)
    {
        Logger.LogInformation($"Processing message: {message}");
    }

    private void EnsureConsumerOptionsValid()
    {
        ArgumentNullException.ThrowIfNull(Options);
        ArgumentNullException.ThrowIfNull(Options.GroupId);
        ArgumentNullException.ThrowIfNull(Options.Topics);
    }

    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}

public class ConsumerOptions
{
    public List<string>? Topics { get; set; } = null;
    public string? GroupId { get; set; } = null;
}