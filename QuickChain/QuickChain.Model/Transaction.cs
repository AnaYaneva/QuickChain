using System;
using System.Collections.Generic;
using System.Text;

namespace QuickChain.Model
{
    public class Transaction : Entity
    {
        public string TxHash { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Decimal Value { get; set; }
        public int UsedGas { get; set; }
        public decimal Fee { get; set; }
        public Guid TransactionIdentifier { get; set; }
        public string SenderPublicKey { get; set; }
        public string Status { get; set; } = "Processing";
        public override string ToString()
        {
            var str = "{";
            str += $@"""TxHash"":""{TxHash}"",""From"":""{From}"",""To"":""{To}"",""Value"":{Value},""UsedGas"":{UsedGas},""Fee"":{Fee}";
            str += "}";
            return str;
        }
    }
}
