using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickChain.Model;

namespace QuickChain.Node.Utils
{
    public class NextBlockComposer : INextBlockComposer
    {
        public void AddTransactionToNextBlock(SignedTransaction transaction)
        {
        }

        public bool IsValidBlock(Block block)
        {
            return true;
        }

        public void RemoveTransactionFromNextBlock(SignedTransaction transaction)
        {
        }
    }
}
