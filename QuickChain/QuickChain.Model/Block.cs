using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace QuickChain.Model
{
    public class Block
    {
        public int Height { get; set; }
        public DateTime TimeStamp { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public string Hash { get; set; }
        public string ParentHash { get; set; }
        public BigInteger Difficulty { get; set; }
        public int Size { get; set; }
    }
}
