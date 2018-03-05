using Microsoft.AspNetCore.Mvc;
using QuickChain.Data;
using QuickChain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    [Route("[controller]")]
    public class BlocksController : Controller
    {
        private IRepository<Block> blockRepository;

        public BlocksController(IRepository<Block> blockRepository)
        {
            this.blockRepository = blockRepository;
        }

        [HttpGet]
        public IEnumerable<Block> GetAll(int? page, int? perPage)
        {

            return this.blockRepository
                .GetAll(false)
                .Skip(((page ?? 1) - 1)*(perPage ?? 20))
                .Take(perPage ?? 20);
        }

        [HttpGet("{id}")]
        public Block Get(int id)
        {
            return this.blockRepository
                .GetById(id);
        }

        [HttpPost()]
        public Block Create([FromBody]Block block)
        {
            Block dbBlock = this.blockRepository.Insert(block);
            this.blockRepository.Save();

            return dbBlock;
        }
    }
}
