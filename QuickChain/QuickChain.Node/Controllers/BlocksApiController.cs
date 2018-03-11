using Microsoft.AspNetCore.Mvc;
using QuickChain.Data;
using QuickChain.Model;
using QuickChain.Node.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    [Route("api/[controller]")]
    public class BlocksApiController : Controller
    {
        private readonly IRepository<Block> blockRepository;
        private readonly IMiningManager miningManager;
        private readonly IHashLibrary hashLibrary;
        private readonly IRepository<SignedTransaction> transactionsRepository;

        public BlocksApiController(IRepository<Block> blockRepository, IMiningManager miningManager, IRepository<SignedTransaction> transactionsRepository, IHashLibrary hashLibrary)
        {
            this.blockRepository = blockRepository;
            this.miningManager = miningManager;
            this.transactionsRepository = transactionsRepository;
            this.hashLibrary = hashLibrary;
        }

        [HttpGet]
        public IEnumerable<Block> GetAll(int? page, int? perPage)
        {
            return this.blockRepository
                .GetAll(false)
                .Skip(((page ?? 1) - 1) * (perPage ?? 20))
                .Take(perPage ?? 20);
        }

        [HttpGet("{id}")]
        public Block Get(int id)
        {
            return this.blockRepository
                .GetById(id);
        }

        [HttpPost()]
        public string Create([FromBody]Block[] blocks)
        {
            if (blocks.Length == 1)
            {
                if (blocks[0].ParentHash == this.miningManager.MinedBlock.ParentHash)
                {
                    this.miningManager.AddBlock(blocks[0]);
                }
                else
                {
                    return "ParentHash does not match. Please provide us with more blocks so that we could compare our chains";
                }
            }

            Block firstCommonBlock = this.blockRepository.GetAll()
                .FirstOrDefault<Block>(b => b.Height == blocks[0].Height);

            if (firstCommonBlock == null)
            {
                throw new Exception("No common block! Please provide older blocks.");
            }

            foreach (Block block in blocks)
            {
                if (!this.hashLibrary.IsValidBlocks(block))
                {
                    // TODO: remove originator from peers;
                    throw new UnauthorizedAccessException("invalid block");
                }
            }

            long theirChainComplexity = blocks.Sum(c => c.Difficulty);
            long ourChainComplexity = this.blockRepository
                .GetAll()
                .Where(b => b.Id >= firstCommonBlock.Id)
                .Sum(c => c.Difficulty);

            if (ourChainComplexity == theirChainComplexity)
            {
                return "Please try again later!";
            }
            else if (ourChainComplexity > theirChainComplexity)
            {
                // TODO: post our chain to the other node
                return "Our blockchain is longer. Please update your blocks";
            }
            else
            {
                this.miningManager.MergeChains(blocks.Where(b => b.Id >= firstCommonBlock.Id));

               return "Blocks successfully added!";
            }
        }
    }
}
