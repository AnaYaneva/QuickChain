using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Model
{
    public class BalanceModel
    {
        public string Address { get; set; }
        public BalanceInfo ConfirmedBalance { get; set; }
        public BalanceInfo LastMinedBalance { get; set; }
        public BalanceInfo PendingBalance { get; set; }
    }
}
