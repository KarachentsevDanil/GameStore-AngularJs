using System.Collections.Generic;

namespace GSP.Domain.Params
{
    public class CollectionResult<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Collection { get; set; }

        public int TotalCount { get; set; }
    }
}