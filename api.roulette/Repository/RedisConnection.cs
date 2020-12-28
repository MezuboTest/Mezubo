using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace api.roulette.Repository
{
    public class RedisConnection : IRedisConnection

    {
        private const string ConnectionString = "localhost:6379";
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;
        private ConnectionMultiplexer Connection => _lazyConnection.Value;
        private IDatabase RedisCache => Connection.GetDatabase();
        public RedisConnection()
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { ConnectionString }
            }));
        }





        public string Get(string id)
        {
            var val = RedisCache.StringGet(id);
            return val.ToString();
        }

        public bool Set(string id, string value)
        {
            return RedisCache.StringSet(id, value);
        }

        public List<string> GetAllKeys()
        {
            List<string> lstRoulette = new List<string>();
            ConfigurationOptions options = ConfigurationOptions.Parse(ConnectionString);
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
            IDatabase db = connection.GetDatabase();
            EndPoint endPoint = connection.GetEndPoints().First();
            RedisKey[] keys = connection.GetServer(endPoint).Keys(pattern: "*").ToArray();
            var server = connection.GetServer(endPoint);
            foreach (var key in server.Keys())
            {
                lstRoulette.Add(Get(key));
            }
            return lstRoulette;
        }



    }
}
