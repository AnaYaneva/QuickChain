using System;

namespace QuickChain.Model
{
    public class MiningJob
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string BlockHash { get; set; }
        public int Difficulty { get; set; }
        public long NounceFrom { get; set; }
        public long NounceTo { get; set; }
    }
}
