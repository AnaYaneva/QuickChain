using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace QuickChain.Model
{
    public class Block : Entity
    {
        public int Height { get; set; }
        public DateTime TimeStamp { get; set; }
        public IList<SignedTransaction> Transactions { get; set; }
        public string Hash { get; set; }
        public string ParentHash { get; set; }
        public long Difficulty { get; set; }
        public long Nounce { get; set; }
        //public int Size { get; set; }
        //public override string ToString()
        //{
        //    //var trStr = "";
        //    //for (int i = 0; i < Transactions.Count; i++)
        //    //{
        //    //    if (i != Transactions.Count - 1)
        //    //    {
        //    //        trStr += Transactions[i].ToString();
        //    //        trStr += ",";
        //    //        continue;
        //    //    }
        //    //    trStr += Transactions[i].ToString();
        //    //}

        //    //var str = "{";
        //    //str += $@"""Height"":{Height},""TimeStamp"":""{TimeStamp}"",""Hash"":""{Hash}"",""ParentHash"":""{ParentHash}"",""Difficulty"":{Difficulty},""Size"":{Size},""Transactions"":[{trStr}]";
        //    //str += "}";
        //    //return str;

        //    return 
        //}
    }
}
