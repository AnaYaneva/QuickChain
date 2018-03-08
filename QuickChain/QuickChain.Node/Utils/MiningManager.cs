using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickChain.Data;
using QuickChain.Model;
using QuickChain.Node.Model;

namespace QuickChain.Node.Utils
{
    public class MiningManager : IMiningManager
    {
        private const long Difficulty = (1 << 10);
        private const long MiningDivisions = (1 << 20);

        private IDictionary<Guid, MiningJob> miningJobs;
        private Block minedBlock;
        private long miningIteration;

        private readonly IRepository<Block> blockRepository;

        public Block MinedBlock
        {
            get
            {
                return this.minedBlock;
            }
        }

        public MiningManager(IRepository<Block> blockRepository)
        {
            this.blockRepository = blockRepository;
            this.miningJobs = new Dictionary<Guid, MiningJob>();
        }

        public void Start()
        {
            this.minedBlock = new Block()
            {
                Transactions = new List<SignedTransaction>(),
            };

            this.RefreshNextBlock();
        }

        private void RefreshNextBlock()
        {
            Block prevBlock = this.blockRepository.GetAll().First();

            var nextBlock = new Block()
            {
                Difficulty = Difficulty,
                Height = prevBlock.Height + 1,
                TimeStamp = DateTime.UtcNow,
                ParentHash = prevBlock.Hash,
                Transactions = new List<SignedTransaction>()
            };

            foreach (SignedTransaction transaction in this.minedBlock.Transactions)
            {
                if (!prevBlock.Transactions.Any(t => t.TransactionHash == transaction.TransactionHash))
                {
                    nextBlock.Transactions.Add(transaction);
                }
            }

            this.minedBlock = nextBlock;
        }

        public void AddTransactionToNextBlock(SignedTransaction transaction)
        {
            if (!this.minedBlock.Transactions.Any(t => t.TransactionHash == transaction.TransactionHash))
            {
                this.minedBlock.Transactions.Add(transaction);
            }

            this.minedBlock.TimeStamp = DateTime.UtcNow;
        }

        public void RemoveTransactionFromNextBlock(string transactionHash)
        {
            SignedTransaction transaction = this.minedBlock.Transactions.FirstOrDefault(t => t.TransactionHash == transactionHash);
            if (transaction != null)
            {
                this.minedBlock.Transactions.Remove(transaction);
                this.minedBlock.TimeStamp = DateTime.UtcNow;
            }
        }

        public MiningJob GetMiningJob(string rewardAddress)
        {
            var job = new MiningJob()
            {
                Id = Guid.NewGuid(),
                RewardAddress = rewardAddress,
                Block = this.MinedBlock,
                NounceFrom = MiningDivisions * this.miningIteration,
                NounceTo = MiningDivisions * (this.miningIteration + 1),
                Difficulty = Difficulty,
            };

            this.miningJobs.Add(job.Id, job);

            this.miningIteration++;
            if (miningIteration > (1 << 60))
            {
                this.miningIteration = 0;
                this.RefreshNextBlock();
            }

            return job;
        }

        public void AddMinedBlock(Guid jobId, long nounce)
        {
            // TODO: ValidateBlock

            this.blockRepository.Insert(this.miningJobs[jobId].Block);
            this.blockRepository.Save();

            // TODO: reward miners

            this.miningJobs.Clear();

            this.RefreshNextBlock();
        }

        public void MergeChains(IEnumerable<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                // TOOD: validate
            }

            // TODO: lock threads
            foreach (Block block in blocks)
            {
                this.blockRepository.Insert(block);
            }

            this.blockRepository.Save();

            this.RefreshNextBlock();
        }
    }
}
