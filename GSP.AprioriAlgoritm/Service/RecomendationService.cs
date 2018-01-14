using GSP.AprioriAlgoritm.Contracts;
using GSP.AprioriAlgoritm.Helper;
using GSP.AprioriAlgoritm.Model;
using System.Collections.Generic;
using System.Linq;

namespace GSP.AprioriAlgoritm.Service
{
    public class RecomendationService : IRecomendationService
    {
        public IEnumerable<int> GetRecomendations(List<int[]> transactions, int totalTransactionsCount, int currentItemId)
        {
            var currentItemSupport = transactions.Count / (double)totalTransactionsCount;

            var itemsetList = GetSet(transactions);
            var defaultSupportValue = 1 / (double)totalTransactionsCount;

            var apriori = new AprioriTransactionsAnalyzer();
            var data = apriori.GetFrequentTransactions(itemsetList, defaultSupportValue);
            var supports = new List<Transaction>();

            foreach (var transaction in data.TransactionsList)
            {
                var support = data.GetSupport(transaction);

                if (support <= defaultSupportValue)
                    continue;

                supports.Add(new Transaction
                {
                    Support = support,
                    TransactionOperation = transaction,
                    Confident = support / currentItemSupport
                });
            }

            var mostConfidentTransactions = GetMostConfidentTransaction(supports);

            if(mostConfidentTransactions == null)
            {
                return Enumerable.Empty<int>();
            }

            return mostConfidentTransactions.TransactionOperation.Where(x => x != currentItemId);
        }

        private List<HashSet<int>> GetSet(List<int[]> item)
        {
            var collection = new List<HashSet<int>>();

            foreach (var element in item)
            {
                var hash = new HashSet<int>(element);
                collection.Add(hash);
            }

            return collection;
        }

        private Transaction GetMostConfidentTransaction(List<Transaction> collection)
        {
            return collection
                .OrderByDescending(x => x.Confident).ThenByDescending(x => x.Support)
                .FirstOrDefault();
        }
    }
}
