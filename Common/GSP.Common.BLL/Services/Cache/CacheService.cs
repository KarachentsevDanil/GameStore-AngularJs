using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using GSP.Common.BLL.Extensions;
using GSP.Common.BLL.Services.Contracts;

namespace GSP.Common.BLL.Services.Cache
{
    public class CacheService : ICacheService
    {
        private const int CacheExpirationInMinutes = 20;
        private static readonly Dictionary<CacheBucket, MemoryCache> CacheBuckets = new Dictionary<CacheBucket, MemoryCache>();

        static CacheService()
        {
            var bucketNames = EnumExtensions.EnumToDictionary<CacheBucket>();
            foreach (var item in bucketNames)
            {
                var cacheBucket = new MemoryCache(item.Value);
                CacheBuckets.Add(item.Key, cacheBucket);
            }
        }

        public void Add<T>(T data, string key, CacheBucket bucket)
        {
            ObjectCache cache = CacheBuckets[bucket];
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(CacheExpirationInMinutes) };
            cache.Set(key, data, policy);
        }

        public T Get<T>(string key, CacheBucket bucket)
        {
            ObjectCache cache = CacheBuckets[bucket];
            if (cache[key] is T)
            {
                var data = (T)cache[key];
                return data;
            }

            return default(T);
        }

        public object Get(string key, CacheBucket bucket)
        {
            ObjectCache cache = CacheBuckets[bucket];
            return cache[key];
        }

        public void CleanAll()
        {
            foreach (CacheBucket cacheBucket in Enum.GetValues(typeof(CacheBucket)))
            {
                EmptyBucket(cacheBucket);
            }
        }
        
        public void Remove(string key, CacheBucket bucket)
        {
            ObjectCache cache = CacheBuckets[bucket];
            cache.Remove(key);
        }

        public void EmptyBucket(CacheBucket bucket)
        {
            var oldBucket = CacheBuckets[bucket];
            CacheBuckets[bucket] = new MemoryCache(bucket.ToString());
            oldBucket.Dispose();
            GC.Collect();
        }
    }
}
