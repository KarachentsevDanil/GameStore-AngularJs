using GSP.AprioriAlgoritm.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSP.AprioriAlgoritm.Helper
{
    public class AprioriTransactionsAnalyzer
    {
        public const double MaximumSupportValue = 1.0;

        public TransactionsSet GetFrequentTransactions(List<HashSet<int>> transactionList, double minimumSupport)
        {
            if (transactionList.Count == 0)
            {
                throw new ArgumentException("List of transactions are is empty.");
            }

            var supportMap = new Dictionary<HashSet<int>, int>();
            var frequentItemList = GetFrequentItems(transactionList, supportMap, minimumSupport);

            var currentTransaction = 1;
            var map = new Dictionary<int, List<HashSet<int>>> { { currentTransaction, frequentItemList } };

            do
            {
                currentTransaction++;
                var candidateList = GenerateCandidates(map[currentTransaction - 1]);

                foreach (var transaction in transactionList)
                {
                    var subCandidateList = GetSubset(candidateList, transaction);

                    foreach (var itemset in subCandidateList)
                    {
                        if (supportMap.TryGetValue(itemset, out _))
                        {
                            supportMap[itemset]++;
                        }
                        else if (!supportMap.ContainsKey(itemset))
                        {
                            supportMap.Add(itemset, 1);
                        }
                    }
                }

                map.Add(currentTransaction, GetNextTransactionsSet(candidateList, supportMap, minimumSupport, transactionList.Count));

            } while (map[currentTransaction].Count != 0);

            var frequentTransaction = new TransactionsSet(ToHeshSetList(map), supportMap, transactionList.Count);

            return frequentTransaction;
        }

        private List<HashSet<int>> ToHeshSetList(Dictionary<int, List<HashSet<int>>> map)
        {
            var hashSetList = new List<HashSet<int>>();

            foreach (var itemsetList in map.Values)
            {
                hashSetList.AddRange(itemsetList);
            }

            return hashSetList;
        }


        private List<HashSet<int>> GetNextTransactionsSet(List<HashSet<int>> candidateList, Dictionary<HashSet<int>, int> supportCountMap, double minimumSupport, int transactions)
        {
            var transactionsSet = new List<HashSet<int>>(candidateList.Count);

            foreach (var itemset in candidateList)
            {
                if (supportCountMap.ContainsKey(itemset))
                {
                    var supportCount = supportCountMap[itemset];
                    var support = MaximumSupportValue * supportCount / transactions;

                    if (support >= minimumSupport)
                    {
                        transactionsSet.Add(itemset);
                    }
                }
            }

            return transactionsSet;
        }

        private List<HashSet<int>> GetSubset(List<HashSet<int>> candidateList, HashSet<int> transaction)
        {
            var ret = new List<HashSet<int>>(candidateList.Count);

            foreach (var candidate in candidateList)
            {
                if (candidate.SequenceEqual(transaction))
                {
                    ret.Add(candidate);
                }
            }

            return ret;
        }

        private List<HashSet<int>> GenerateCandidates(List<HashSet<int>> itemsetList)
        {
            var list = new List<List<int>>(itemsetList.Count);

            foreach (HashSet<int> itemset in itemsetList)
            {
                var l = new List<int>(itemset);
                l.Sort();
                list.Add(l);
            }

            int listSize = list.Count;
            var ret = new List<HashSet<int>>(listSize);

            for (int i = 0; i < listSize; ++i)
            {
                for (int j = i + 1; j < listSize; ++j)
                {
                    var candidate = TryMergeItemsets(list[i], list[j]);

                    if (candidate != null)
                    {
                        ret.Add(candidate);
                    }
                }
            }

            return ret;
        }

        private HashSet<int> TryMergeItemsets(List<int> firstSet, List<int> secondSet)
        {

            int length = firstSet.Count;

            for (int i = 0; i < length - 1; ++i)
            {
                if (!firstSet[i].Equals(secondSet[i]))
                {
                    return null;
                }
            }

            if (firstSet[length - 1].Equals(secondSet[length - 1]))
            {
                return null;
            }

            var ret = new HashSet<int>();

            for (int i = 0; i < length - 1; ++i)
            {
                ret.Add(firstSet[i]);
            }

            ret.Add(firstSet[length - 1]);
            ret.Add(secondSet[length - 1]);
            return ret;
        }

        private List<HashSet<int>> GetFrequentItems(List<HashSet<int>> itemsetList, Dictionary<HashSet<int>, int> supportCountMap, double minimumSupport)
        {
            var map = new Dictionary<int, int>();

            foreach (var itemset in itemsetList)
            {
                foreach (var item in itemset)
                {
                    var tmp = new HashSet<int>();
                    tmp.Add(item);

                    if (supportCountMap.Keys.Any(x => x.Contains(item)))
                    {
                        var key = supportCountMap.Keys.FirstOrDefault(x => x.Contains(item));
                        supportCountMap[key]++;
                    }
                    else
                    {
                        supportCountMap.Add(tmp, 1);
                    }
                    int value = 0;
                    if (map.TryGetValue(item, out value))
                        map[item]++;
                    else if (!map.ContainsKey(item))
                        map.Add(item, 0 + 1);
                }
            }

            var frequentItemsetList = new List<HashSet<int>>();

            foreach (KeyValuePair<int, int> entry in map)
            {
                if (1.0 * entry.Value / map.Count >= minimumSupport)
                {
                    var itemset = new HashSet<int>();
                    itemset.Add(entry.Key);
                    frequentItemsetList.Add(itemset);
                }
            }

            return frequentItemsetList;
        }
    }
}
