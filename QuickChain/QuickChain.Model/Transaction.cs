using System;
using System.Collections.Generic;
using System.Text;

namespace QuickChain.Model
{
    public class Transaction : Entity
    {
        public string TransactionHash { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Decimal Value { get; set; }
        public decimal Fee { get; set; }
        public Guid TransactionIdentifier { get; set; }
        public string SenderPublicKey { get; set; }
        public DateTime Time { get; set; }
        public override string ToString()
        {
            var str = "{";
            str += $@"""TxHash"":""{TransactionHash}"",""From"":""{From}"",""To"":""{To}"",""Value"":{Value},""Fee"":{Fee}";
            str += "}";
            return str;
        }
    }
}
