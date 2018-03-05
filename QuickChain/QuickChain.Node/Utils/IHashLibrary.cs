using QuickChain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Utils
{
    public interface IHashLibrary
    {
        string GetHash(SignedTransaction transaction);
        bool IsValidSignature(SignedTransaction transaction, string r, string s);

    }
}
