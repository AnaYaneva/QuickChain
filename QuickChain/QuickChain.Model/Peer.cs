using QuickChain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Model
{
    public class Peer : Entity
    {
        public string Url { get; set; }
        public long ConnectedOnBlock { get; set; }
    }
}
