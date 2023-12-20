using System.Threading.Channels;

namespace Water.Web
{
    public class StreamMessage<T> : IAsyncEnumerable<T>
    {
        private readonly Channel<T> channel;

        public StreamMessage()
            : this(Channel.CreateUnbounded<T>(new UnboundedChannelOptions { SingleReader = true }))
        {
        }

        public StreamMessage(Channel<T> channel)
        {
            this.channel = channel;
        }

        public ChannelWriter<T> Writer => channel.Writer;

        public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = await channel.Reader.WaitToReadAsync(cancellationToken);
                if (result && channel.Reader.TryRead(out var item))
                {
                    yield return item;
                }
            }
        }
    }
}
