namespace QuickChain.Miner.Models
{
    public class BlockTemplate
    {
        public int Index { get; set; }

        public decimal ExpectedReward { get; set; }

        public string PrevBlockHash { get; set; }

        public string TransactionHash { get; set; }

        public int Difficulty { get; set; }
    }
}
