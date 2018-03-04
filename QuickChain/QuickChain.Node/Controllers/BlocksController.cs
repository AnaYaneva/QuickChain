using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public IEnumerable<Block> GetAll()
        {
            return new Block[] { };
        }

        [HttpGet("{id}")]
        public Block Get(int id)
        {
            return new Block();
        }

        [HttpPost()]
        public Block Create([FromBody]Block block)
        {
            return block;
        }
    }
}
