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
        private const int Difficulty = 1;
        private const long MiningDivisions = (1 << 25);

        private IDictionary<Guid, MiningJob> miningJobs;
        private IDictionary<Guid, Block> blockForMiningJob;
        private Block minedBlock;
        private long miningIteration;

        private readonly IRepository<Block> blockRepository;
        private readonly IRepository<SignedTransaction> transactionsRepository;
        private readonly IHashLibrary hashLibrary;

        public Block MinedBlock
        {
            get
            {
                return this.minedBlock;
            }
        }

        public MiningManager(IRepository<Block> blockRepository, IHashLibrary hashLibrary, IRepository<SignedTransaction> transactionsRepository)
        {
            this.blockRepository = blockRepository;
            this.hashLibrary = hashLibrary;
            this.transactionsRepository = transactionsRepository;

            this.miningJobs = new Dictionary<Guid, MiningJob>();
            this.blockForMiningJob = new Dictionary<Guid, Block>();
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
                TimeStamp = DateTime.UtcNow,
                Height = prevBlock.Height + 1,
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
                Difficulty = Difficulty,
                Index = this.MinedBlock.Height,
                BlockHash = this.hashLibrary.GetHash(this.MinedBlock),
                NonceFrom = MiningDivisions * this.miningIteration,
                NonceTo = MiningDivisions * (this.miningIteration + 1),
            };

            this.miningJobs.Add(job.Id, job);

            // TODO: lock threads
            var transactions = new List<SignedTransaction>();
            foreach (SignedTransaction transaction in this.MinedBlock.Transactions)
            {
                transactions.Add(transaction);
            }
            this.blockForMiningJob.Add(job.Id, new Block()
            {
                Difficulty = this.MinedBlock.Difficulty,
                TimeStamp = this.MinedBlock.TimeStamp,
                Height = this.MinedBlock.Height,
                ParentHash = this.MinedBlock.ParentHash,
                Transactions = transactions,
            });

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

            if (this.blockForMiningJob.ContainsKey(jobId))
            {
                this.blockRepository.Insert(this.blockForMiningJob[jobId]);
                this.blockRepository.Save();
            }

            // TODO: reward miners

            this.miningJobs.Clear();
            this.blockForMiningJob.Clear();

            this.RefreshNextBlock();
        }

        public void MergeChains(IEnumerable<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                // delete old
                Block oldBlock = this.blockRepository.GetAll().First(b => b.Height == block.Height);
                IEnumerable<SignedTransaction> oldSignedTransaction = this.transactionsRepository
                    .GetAll()
                    .Where(t => t.BlockHeight == oldBlock.Height);
                foreach(SignedTransaction transaction in oldSignedTransaction)
                {
                    this.transactionsRepository.Delete(transaction);
                }

                // add new
                Block dbBlock = this.blockRepository.Insert(block);
                foreach (SignedTransaction transaction in dbBlock.Transactions)
                {
                    this.transactionsRepository.Insert(transaction);
                    this.RemoveTransactionFromNextBlock(transaction.TransactionHash);
                }
            }

            this.transactionsRepository.Save();
            this.blockRepository.Save();

            this.RefreshNextBlock();
        }

        public void AddBlock(Block block)
        {
            if(block.ParentHash != this.minedBlock.ParentHash)
            {
                throw new Exception("this is not the next block - the hashes does not match");
            }

            this.blockRepository.Insert(block);
            this.blockRepository.Save();

            foreach(SignedTransaction transaction in block.Transactions)
            {
                this.transactionsRepository.Insert(transaction);
                this.RemoveTransactionFromNextBlock(transaction.TransactionHash);
            }
            this.transactionsRepository.Save();
        }
    }
}
