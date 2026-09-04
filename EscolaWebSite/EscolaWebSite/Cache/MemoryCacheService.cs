using System;
using System.Runtime.Caching;
using EscolaWebSite.Cache.Interfaces;

namespace EscolaWebSite.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _defaultPolicy;

        public MemoryCacheService()
        {
            _defaultPolicy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
            };
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Chave inválida");

            var value = _cache.Get(key);
            if (value == null)
                return default(T);

            return (T)value;
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Chave inválida");

            var policy = expiration.HasValue
                ? new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.Add(expiration.Value) }
                : _defaultPolicy;

            _cache.Set(key, value, policy);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            _cache.Remove(key);
        }

        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            return _cache.Contains(key);
        }

        public void RemoveByPattern(string pattern)
        {
            // Implementação simples para remover por padrão
            foreach (var key in _cache)
            {
                if (key.Key.Contains(pattern))
                    _cache.Remove(key.Key);
            }
        }
    }
}