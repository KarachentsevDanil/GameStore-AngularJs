using System.Collections.Generic;

namespace GSP.AprioriAlgoritm.Model
{
    public class TransactionsSet
    {
        public List<HashSet<int>> TransactionsList { get; set; }
        public Dictionary<HashSet<int>, int> SupportDictionary { get; set; }
        public double TransactionCount { get; set; }

        public TransactionsSet(List<HashSet<int>> transactionsList, Dictionary<HashSet<int>, int> supportDictionary, int transactionCount)
        {
            TransactionsList = transactionsList;
            SupportDictionary = supportDictionary;
            TransactionCount = transactionCount;
        }

        public double GetSupport(HashSet<int> set)
        {
            return SupportDictionary.ContainsKey(set) ? 1.0 * SupportDictionary[set] / TransactionCount : 0;
        }
    }
}
