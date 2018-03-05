using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Model
{
    public class TransactionModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Value { get; set; }
        public string SenderPublicKey { get; set; }
    }
}
