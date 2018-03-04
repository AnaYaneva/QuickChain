using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Model
{
    public class InfoModel
    {
        public string About { get; set; }
        public string NodeUrl { get; set; }
        public int Peers { get; set; }
        public string CurrentDifficulty { get; set; }
        public string CumulativeDifficulty { get; set; }
        public int Blocks { get; set; }
        public int ConfirmedTransactions { get; set; }
        public int PendingTransactions { get; set; }
        public int TransactionsNeededForConfirmation { get; set; }
    }
}