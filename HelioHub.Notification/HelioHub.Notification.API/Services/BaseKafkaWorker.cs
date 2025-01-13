using Confluent.Kafka;
using System.Text.Json;

namespace HelioHub.Notification.API.Services
{
        public abstract class BaseKafkaWorker<T> : BackgroundService
        {
            private bool _disposed;

            public BaseKafkaWorker() => _disposed = false;

            protected abstract string GetTopicName();
            protected abstract Task ProcessMessage(T msg);


            protected override async Task ExecuteAsync(CancellationToken cancellationToken)
            {
                Task.Run(async () => await HandleMessageAsync(cancellationToken));
            }


            private async Task HandleMessageAsync(CancellationToken cancellationToken)
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "email_consumer",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                };

                using (var consumer = new ConsumerBuilder<Ignore, string>(config).SetValueDeserializer(Deserializers.Utf8).Build())
                {
                    consumer.Subscribe(GetTopicName());

                    while (!_disposed)
                    {
                        var message = JsonSerializer.Deserialize<T>(consumer.Consume(cancellationToken).Message.Value);

                        await ProcessMessage(message!);
                    }

                    consumer.Close();
                }
            }
        }
}
