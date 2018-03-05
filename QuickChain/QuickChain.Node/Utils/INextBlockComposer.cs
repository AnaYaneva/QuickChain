using QuickChain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Utils
{
    public interface INextBlockComposer
    {
        void AddTransactionToNextBlock(SignedTransaction transaction);
        void RemoveTransactionFromNextBlock(SignedTransaction transaction);
        bool IsValidBlock(Block block);
    }
}
