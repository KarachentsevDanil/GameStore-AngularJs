using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GSP.Domain.Params;

namespace GSP.DAL.Repositories.Contracts
{
    public interface IExperssionable<T> where T : class
    {
        IEnumerable<T> GetItems(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetItemsByParams(FilterParams<T> filterParams);
    }
}
