using Confluent.Kafka;

namespace BarsGroupProjectN1.Core.Extensions;

public static class ConsumerExtensions
{
    public static async ValueTask<ConsumeResult<TKey, TValue>> ConsumeAsync<TKey, TValue>(this IConsumer<TKey, TValue> consumer, CancellationToken ct)
    {
        try
        {
            var res = consumer.Consume(0);
            if (res != null)
            {
                return res;
            }

            return await Task.Run(() => consumer.Consume(ct), ct);
        }
        catch (OperationCanceledException)
        {
            throw new TaskCanceledException($"Kafka consumer [{consumer.Name}] was canceled.");
        }
        catch (Exception e)
        {
            throw new Exception($"Kafka consumer [{consumer.Name}] has failed.", e);
        }
    }
}