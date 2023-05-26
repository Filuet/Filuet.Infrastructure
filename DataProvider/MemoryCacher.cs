using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Filuet.Infrastructure.DataProvider
{
    public class MemoryCacher
    {
        public MemoryCache Cache { get; set; }

        public IDictionary<string, DateTime> KeysClearTime { get; set; }

        public event EventHandler<CacheClearEventArgs> OnCacheClear;

        private uint _sizeMb = 1;

        public MemoryCacher(uint sizeMb = 1)
        {
            _sizeMb = sizeMb;
            KeysClearTime = new Dictionary<string, DateTime>();
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024 * 1000 * _sizeMb
            });
        }

        public T Get<T>(object key) => Cache.Get<T>(key);

        public T Set<T>(object key, T value, double minDuration = 60000, bool riseEvent = true)
        {
            if (value == null && riseEvent)
                OnCacheClear.Invoke(this, new CacheClearEventArgs(key.ToString()));

            KeysClearTime[key.ToString()] = DateTime.Now;

            return Cache.Set(key, value, new MemoryCacheEntryOptions { Size = 1000, AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(minDuration) });
        }

        public void Clear()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024 * 1000 * _sizeMb
            });
        }
    }

    public class CacheClearEventArgs : EventArgs
    {
        public string CacheKey { get; set; }

        public CacheClearEventArgs(string cacheKey)
        {
            CacheKey = cacheKey;
        }
    }
}
