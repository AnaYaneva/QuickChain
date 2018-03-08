using QuickChain.Model;
using QuickChain.Node.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Utils
{
    public interface IMiningManager
    {
        Block MinedBlock { get; }
        void Start();
        MiningJob GetMiningJob(string rewardAddress);
        void AddTransactionToNextBlock(SignedTransaction transaction);
        void RemoveTransactionFromNextBlock(string transactionHash);
        void AddMinedBlock(Guid jobId, long nounce);
        void MergeChains(IEnumerable<Block> blocks);
    }
}
