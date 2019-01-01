using System.Collections.Generic;

namespace GSP.AprioriAlgoritm.Model
{
    public class Transaction
    {
        public double Support { get; set; }
        public double Confident { get; set; }
        public HashSet<int> TransactionOperation { get; set; }
    }
}
