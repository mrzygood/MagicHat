using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace MagicHat.Messaging.RabbitMQ.Connection;

public interface IChannelsPool
{
    IModel Get();
    void Return(IModel channel);
}

public sealed class ChannelsPool : IChannelsPool 
{
    private readonly IConnection _connection;
    private readonly ConcurrentQueue<IModel> _channels = new();
    private readonly int _maxChannelsAmount = 100; // TODO initialize from configuration
    private int _poolSize;
    private readonly object _lockObject = new();

    public ChannelsPool(IConnection connection)
    {
        _connection = connection;
    }
    
    public IModel Get()
    {
        lock (_lockObject)
        {
            if (_channels.TryDequeue(out var channel))
            {
                return channel;
            }

            if (_poolSize >= _maxChannelsAmount)
            {
                throw new Exception($"Exceeded maximum ({_maxChannelsAmount}) amount of allowed channels in pool");
            }

            _poolSize++;

            return _connection.CreateModel();
        }
    }

    public void Return(IModel channel)
    {
        lock (_lockObject)
        {
            if (channel.IsOpen is false)
            {
                _poolSize--;
                return;
            }
            
            _channels.Enqueue(channel);
        }
    }
}
