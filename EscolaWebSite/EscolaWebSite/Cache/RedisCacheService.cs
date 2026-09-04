using System;
using System.Configuration;
using System.Data.Entity;
using System.Text.Json;
using StackExchange.Redis;
using EscolaWebSite.Cache.Interfaces;

namespace EscolaWebSite.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public RedisCacheService()
        {
            var redisConnection = ConfigurationManager.AppSettings["RedisConnection"] ?? "localhost:6379";
            _redis = ConnectionMultiplexer.Connect(redisConnection);
            _db = _redis.GetDatabase();
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Chave inválida");

            var value = _db.StringGet(key);
            if (!value.HasValue)
                return default(T);

            return JsonSerializer.Deserialize<T>(value);
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Chave inválida");

            var json = JsonSerializer.Serialize(value);
            var expiry = expiration ?? TimeSpan.FromMinutes(5);

            _db.StringSet(key, json, expiry);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            _db.KeyDelete(key);
        }

        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            return _db.KeyExists(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var server = _redis.GetServer(_redis.GetEndPoints()[0]);
            var keys = server.Keys(pattern: $"*{pattern}*");

            foreach (var key in keys)
            {
                _db.KeyDelete(key);
            }
        }
    }
}