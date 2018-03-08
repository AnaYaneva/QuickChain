using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuickChain.Model;

namespace QuickChain.Node.Model
{
    public class MiningJob
    {
        public Guid Id { get; set; }
        public long NounceFrom { get; set; }
        public long NounceTo { get; set; }
        public Block Block { get; set; }
        public string RewardAddress { get; set; }
        public long Difficulty { get; set; }
    }
}
