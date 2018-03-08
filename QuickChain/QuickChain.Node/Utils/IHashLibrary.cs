using QuickChain.Model;
using QuickChain.Node.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Utils
{
    public interface IHashLibrary
    {
        string GetHash(TransactionModel transaction);
        string GetHash(Block block);
        bool IsValidSignature(TransactionModel transaction, string r, string s);

    }
}
