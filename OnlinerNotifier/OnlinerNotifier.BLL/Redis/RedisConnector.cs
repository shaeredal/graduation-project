using System;
using StackExchange.Redis;

namespace OnlinerNotifier.BLL.Redis
{
    public class RedisConnector
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection => lazyConnection.Value;

        static RedisConnector()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
        }
    }
}
