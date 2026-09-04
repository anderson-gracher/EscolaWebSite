using EscolaWebSite.Cache;
using EscolaWebSite.Cache.Interfaces;
using System.Configuration;

namespace EscolaWebSite.Helpers
{
    public static class CacheHelper
    {
        public static ICacheService GetCacheService()
        {
            var provider = ConfigurationManager.AppSettings["CacheProvider"] ?? "Memory";

            // TODO: Implementar lógica para escolher o provedor de cache com base na configuração
            //return provider.ToUpper() switch
            //{
            //    "REDIS" => new RedisCacheService(),
            //    _ => new MemoryCacheService()
            //};

            return new MemoryCacheService();            
        }
    }
}