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
        public Guid TransactionIdentifier { get; set; }
        public decimal Fee { get; set; }
        public string SignatureR { get; set; }
        public string SignatureS { get; set; }
        public string TransactionHash { get; set; }
    }
}
