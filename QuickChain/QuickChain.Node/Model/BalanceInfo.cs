using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Model
{
    public class BalanceInfo
    {
        public decimal Balance { get; set; }
        public int Confirmations { get; set; }
    }
}
