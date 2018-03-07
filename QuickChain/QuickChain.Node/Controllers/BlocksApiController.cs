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
        private readonly INextBlockComposer nextBlockComposer;
        private readonly IRepository<SignedTransaction> transactionsRepository;

        public BlocksApiController(IRepository<Block> blockRepository, INextBlockComposer nextBlockComposer, IRepository<SignedTransaction> transactionsRepository)
        {
            this.blockRepository = blockRepository;
            this.nextBlockComposer = nextBlockComposer;
            this.transactionsRepository = transactionsRepository;
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
            // TODO: check chain complexity

            foreach (Block block in blocks)
            {
                if (!this.nextBlockComposer.IsValidBlock(block))
                {
                    // TODO: remove originator from peers;
                    throw new UnauthorizedAccessException("invalid block");
                }
            }

            foreach (Block block in blocks)
            {
                Block dbBlock = this.blockRepository.Insert(block);
                foreach(SignedTransaction transaction in dbBlock.Transactions)
                {
                    this.transactionsRepository.Insert(transaction);
                    this.nextBlockComposer.RemoveTransactionFromNextBlock(transaction);
                }
            }

            this.transactionsRepository.Save();
            this.blockRepository.Save();

            return "Blocks successfully added!";
        }
    }
}
