using RabbitMQ.Client;

namespace MagicHat.Messaging.RabbitMQ.Connection;

internal class ChannelAccessor
{
    private static readonly ThreadLocal<ChannelHolder> Holder = new ();

    public IModel? Channel
    {
        get => Holder.Value?.Channel;
        set
        {
            var holder = Holder.Value;
            if (holder != null)
            {
                holder.Channel = null;
            }

            if (value is not null)
            {
                Holder.Value = new ChannelHolder { Channel = value };
            }
        }
    }

    private class ChannelHolder
    {
        public IModel? Channel;
    }
}
