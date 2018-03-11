using System;
using System.Collections.Generic;
using System.Text;

namespace QuickChain.Model
{
    public class MinedHash
    {
        public Guid MiningJobId { get; set; }
        public long Nonce { get; set; }
    }
}
