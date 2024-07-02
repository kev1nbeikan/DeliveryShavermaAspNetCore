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


    protected KafkaConsumerService(ILogger<KafkaConsumerService> logger, IConfiguration configuration)
    {
        Logger = logger;
        _configuration = configuration;
    }

    protected virtual void Configurate()
    {
        var kafkaOptions = _configuration.GetSection("KafkaOptions").Get<KafkaOptions>();
        ArgumentNullException.ThrowIfNull(kafkaOptions, "KafkaOptions not found");

        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = kafkaOptions.BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = GroupId(),
        };
    }

    private void SetConsumer()
    {
        _consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build();
        _consumer.Subscribe(Topic());
    }

    protected abstract string Topic();

    protected abstract string GroupId();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Configurate();
        SetConsumer();
        
        
        Logger.LogInformation("Kafka-consumer started");
        ArgumentNullException.ThrowIfNull(_consumer);
        ArgumentNullException.ThrowIfNull(_consumerConfig);


        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                Logger.LogInformation("Kafka-consumer is working");

                var consumeResult = await _consumer.ConsumeAsync(stoppingToken);
                await ProcessMessageAsync(consumeResult.Message.Value);
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

    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}