using Microsoft.AspNetCore.Mvc;
using QuickChain.Data;
using QuickChain.Model;
using QuickChain.Node.Model;
using QuickChain.Node.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    [Route("api/[controller]")]
    public class MiningJobs : Controller
    {
        private readonly IMiningManager miningManager;

        public MiningJobs(IMiningManager miningManager)
        {
            this.miningManager = miningManager;
        }

        [HttpPost("request")]
        public MiningJob TakeJob(string rewardAddress)
        {
            return this.miningManager.GetMiningJob(rewardAddress);
        }

        [HttpPost("complete")]
        public object CompleteJob(Guid jobId, long nonce)
        {
            this.miningManager.AddMinedBlock(jobId, nonce);

            return "Thank you! The server will validate the block and reward all miners if it is valid";
        }
    }
}
