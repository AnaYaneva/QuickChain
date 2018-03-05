using Microsoft.AspNetCore.Mvc;
using QuickChain.Data;
using QuickChain.Model;
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
        private readonly IRepository<Peer> peersRespository;

        public PeersController(IRepository<Peer> peersRespository)
        {
            this.peersRespository = peersRespository;
        }

        [HttpGet()]
        public IEnumerable<PeerModel> GetAll()
        {
            long currentBlock = 1000; // TODO: implement blocks

            return this.peersRespository
                .GetAll()
                .Select(p => new PeerModel() { Url = p.Url, MinedBlocksSinceConnected = currentBlock - p.ConnectedOnBlock });
        }

        [HttpPost()]
        public PeerModel Create([FromBody]string peerUrl)
        {
            Peer dbPeer = this.peersRespository.Insert(new Peer() { Url = peerUrl });
            this.peersRespository.Save();

            return new PeerModel() { Url = peerUrl, MinedBlocksSinceConnected = 0 };
        }
    }
}
