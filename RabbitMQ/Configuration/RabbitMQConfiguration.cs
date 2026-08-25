namespace ERPBlazorApp.RabbitMQ.Configuration;

public class RabbitMQConfiguration
{
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public string VirtualHost { get; set; } = "/";
    public int RetryCount { get; set; } = 3;
    public int RetryDelayMilliseconds { get; set; } = 500;
    public bool UseBackgroundThreads { get; set; } = true;
}
