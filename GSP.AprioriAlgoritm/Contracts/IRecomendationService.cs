using System.Collections.Generic;

namespace GSP.AprioriAlgoritm.Contracts
{
    public interface IRecomendationService
    {
        IEnumerable<int> GetRecomendations(List<int[]> transactions, int totalTransactionsCount, int currentItemId);
    }
}
