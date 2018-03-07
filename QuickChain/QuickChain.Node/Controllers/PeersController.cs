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
    [Route("api/[controller]")]
    public class PeersController : Controller
    {
        private readonly IRepository<Peer> peersRespository;
        private readonly IRepository<Block> blocksRepository;

        public PeersController(IRepository<Peer> peersRespository, IRepository<Block> blocksRepository)
        {
            this.peersRespository = peersRespository;
            this.blocksRepository = blocksRepository;
        }

        [HttpGet()]
        public IEnumerable<PeerModel> GetAll()
        {
            long currentBlock = this.blocksRepository.GetAll(false).Last().Height;

            return this.peersRespository
                .GetAll()
                .Select(p => new PeerModel()
                    {
                        Url = p.Url,
                        MinedBlocksSinceConnected = currentBlock - p.ConnectedOnBlock
                    }
                );
        }

        [HttpPost()]
        public PeerModel Create([FromBody]string peerUrl)
        {
            Peer dbPeer = this.peersRespository.Insert(new Peer() { Url = peerUrl });
            this.peersRespository.Save();

            return new PeerModel()
            {
                Url = peerUrl,
                MinedBlocksSinceConnected = 0
            };
        }
    }
}
