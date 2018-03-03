﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace QuickChain.Model
{
    public class Block
    {
        public int Height { get; set; }
        public DateTime TimeStamp { get; set; }
        public IList<Transaction> Transactions { get; set; } = new List<Transaction>();
        public string Hash { get; set; }
        public string ParentHash { get; set; }
        public BigInteger Difficulty { get; set; }
        public int Size { get; set; }
        public override string ToString()
        {
            var trStr = "";
            for (int i = 0; i < Transactions.Count; i++)
            {
                if (i != Transactions.Count - 1)
                {
                    trStr += Transactions[i].ToString();
                    trStr += ",";
                    continue;
                }
                trStr += Transactions[i].ToString();
            }

            var str = "{";
            str += $@"""Height"":{Height},""TimeStamp"":""{TimeStamp}"",""Hash"":""{Hash}"",""ParentHash"":""{ParentHash}"",""Difficulty"":{Difficulty},""Size"":{Size},""Transactions"":[{trStr}]";
            str += "}";
            return str;
        }
    }
}
