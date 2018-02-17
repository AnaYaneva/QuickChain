using System;
using System.Collections.Generic;
using System.Text;

namespace QuickChain.Model
{
    public class Transaction
    {
        public string TxHash { get; set; }
        public int BlockHeight { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Decimal Value { get; set; }
        public int UsedGas { get; set; }
        public decimal Fee { get; set; }

    }
}
