using System;
using System.Linq.Expressions;

namespace GSP.Domain.Params
{
    public class FilterParams<TEntity> where TEntity : class
    {
        public int PageSize { get; set; } = 25;

        public int PageNumber { get; set; } = 1;

        public Expression<Func<TEntity, bool>> Expression { get; set; }
    }
}