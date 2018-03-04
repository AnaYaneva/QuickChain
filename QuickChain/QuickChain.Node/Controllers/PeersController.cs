using Microsoft.AspNetCore.Mvc;
using QuickChain.Node.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    [Route("[controller]")]
    public class PeersController : Controller
    {
        [HttpGet()]
        public IEnumerable<Peer> GetAll()
        {
            return new Peer[] { };
        }

        [HttpPost()]
        public Peer Create(Peer peer)
        {
            return new Peer() { };
        }
    }
}
