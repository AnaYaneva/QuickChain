using System;

namespace QuickChain.Model
{
    public class MiningJob
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string DataHash { get; set; }
        public int Difficulty { get; set; }
        public long NonceFrom { get; set; }
        public long NonceTo { get; set; }
    }
}
