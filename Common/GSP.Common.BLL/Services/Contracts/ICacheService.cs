using GSP.Common.BLL.Services.Cache;

namespace GSP.Common.BLL.Services.Contracts
{
    public interface ICacheService
    {
        void Add<T>(T data, string key, CacheBucket bucket);

        T Get<T>(string key, CacheBucket bucket);

        object Get(string key, CacheBucket bucket);

        void CleanAll();

        void Remove(string key, CacheBucket bucket);

        void EmptyBucket(CacheBucket bucket);
    }
}
