using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Options;
using ERPBlazorApp.RabbitMQ.Configuration;

namespace ERPBlazorApp.RabbitMQ.Services;

public class RabbitMQService : IDisposable
{
    private readonly RabbitMQConfiguration _config;
    private readonly Serilog.ILogger _logger = Serilog.Log.ForContext<RabbitMQService>();
    private IConnection? _connection;
    private IModel? _channel;
    private bool _disposed;

    public RabbitMQService(IOptions<RabbitMQConfiguration> config)
    {
        _config = config.Value;
        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                Port = _config.Port,
                UserName = _config.UserName,
                Password = _config.Password,
                VirtualHost = _config.VirtualHost,
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare("erp-blazor", ExchangeType.Topic, durable: true);
            _channel.QueueDeclare("erp-blazor-sales", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueDeclare("erp-blazor-inventory", durable: true, exclusive: false, autoDelete: false);
            _channel.QueueDeclare("erp-blazor-notifications", durable: true, exclusive: false, autoDelete: false);

            _channel.QueueBind("erp-blazor-sales", "erp-blazor", "sales.*");
            _channel.QueueBind("erp-blazor-inventory", "erp-blazor", "inventory.*");
            _channel.QueueBind("erp-blazor-notifications", "erp-blazor", "notification.*");

            _logger.Information("RabbitMQ connection established successfully");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to initialize RabbitMQ connection");
            throw;
        }
    }

    public async Task PublishAsync(string routingKey, string message, string? exchange = "erp-blazor")
    {
        if (_channel == null || _connection == null || !_connection.IsOpen)
        {
            _logger.Warning("RabbitMQ connection is not open. Attempting to reconnect...");
            InitializeRabbitMQ();
        }

        var body = System.Text.Encoding.UTF8.GetBytes(message);

        var properties = _channel!.CreateBasicProperties();
        properties.Persistent = true;
        properties.MessageId = Guid.NewGuid().ToString();
        properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        _channel.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            basicProperties: properties,
            body: body);

        await Task.CompletedTask;
        _logger.Information("Message published to {RoutingKey}: {Message}", routingKey, message);
    }

    public void StartConsuming(string queueName, Func<string, Task> onMessageReceived)
    {
        if (_channel == null || _connection == null || !_connection.IsOpen)
        {
            _logger.Warning("RabbitMQ connection is not open. Cannot start consuming.");
            return;
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var message = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.Information("Message received from {Queue}: {Message}", queueName, message);
                
                await onMessageReceived(message);
                
                _channel.BasicAck(ea.DeliveryTag, multiple: false);
                _logger.Information("Message acknowledged from {Queue}", queueName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error processing message from {Queue}", queueName);
                _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        _logger.Information("Started consuming messages from queue: {Queue}", queueName);
    }

    public void Dispose()
    {
        if (_disposed) return;

        try
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
            _logger.Information("RabbitMQ connection disposed");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error disposing RabbitMQ connection");
        }

        _disposed = true;
    }
}
