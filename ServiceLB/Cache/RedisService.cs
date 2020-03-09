using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class RedisService : ICacheService
    {
        private readonly IDatabase db;

        public RedisService(string host, string pass)
        {
            ConnectionMultiplexer muxer = ConnectionMultiplexer
                                            .Connect($"{host}" +
                                            $",password={pass}");
            db = muxer.GetDatabase();
        }

        public async Task<string> Get(string key)
        {
            var result = await db.StringGetAsync(key).ConfigureAwait(false);
            if (string.IsNullOrEmpty(result))
            {
                result = string.Empty;
            }
            return result;
        }

        public async Task<bool> Set(string key, string value)
        {
            return await db.StringSetAsync(key, value).ConfigureAwait(false);
        }

        public async Task<bool> Delete(string key)
        {
            return await db.KeyDeleteAsync(key).ConfigureAwait(false);
        }
    }
}