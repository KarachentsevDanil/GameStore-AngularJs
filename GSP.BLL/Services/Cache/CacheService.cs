using System;
using System.Collections.Generic;
using GSP.BLL.Extension;
using GSP.BLL.Services.Contracts;
using System.Runtime.Caching;

namespace GSP.BLL.Services.Cache
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

        public void ResetBucket(string key)
        {
            Remove(key, CacheBucket.Bucket);
        }

        public void ResetCategories()
        {
            EmptyBucket(CacheBucket.Categories);
        }
        
        private void Remove(string key, CacheBucket bucket)
        {
            ObjectCache cache = CacheBuckets[bucket];
            cache.Remove(key);
        }

        private void EmptyBucket(CacheBucket bucket)
        {
            var oldBucket = CacheBuckets[bucket];
            CacheBuckets[bucket] = new MemoryCache(bucket.ToString());
            oldBucket.Dispose();
            GC.Collect();
        }
    }
}
