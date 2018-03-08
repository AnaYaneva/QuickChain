namespace QuickChain.Miner
{
    using Infrastructure;
    using Newtonsoft.Json;
    using QuickChain.Miner.Models;
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using System.Text;

    class Miner
    {
        const string hexAlphabet = "0123456789ABCDEF";

        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            TimeSpan maxBlockTime = new TimeSpan(0, 0, 5);

            while (true)
            {
                timer.Start();

                string responseFromNode = MinerToNode.GetRequestToNode();

                BlockTemplate blockTemplate = JsonConvert.DeserializeObject<BlockTemplate>(responseFromNode);
                
                Console.WriteLine("\nStart new task:");
                Console.WriteLine($"Index of block to mine: {blockTemplate.Index}");
                Console.WriteLine($"Expected Reward: {blockTemplate.ExpectedReward}");
                Console.WriteLine($"TransactionsHash: { blockTemplate.TransactionHash}");
                Console.WriteLine($"PrevBlockHash: {blockTemplate.PrevBlockHash}");
                Console.WriteLine($"Difficulty: {blockTemplate.Difficulty}\n");


                bool blockFount = false;
                long nonce = 0;
                string precomputedData = blockTemplate.Index + blockTemplate.TransactionHash + blockTemplate.PrevBlockHash;
                string timestamp = DateTime.UtcNow.ToString("o");

                while (!blockFount && nonce < long.MaxValue)
                {
                    string data = precomputedData + timestamp + nonce;

                    string blockHash = BytesArrayToHexString(Sha256(Encoding.UTF8.GetBytes(data)));

                    bool isPoW = ProofOfWork(precomputedData,blockTemplate,blockHash,timestamp,nonce);
                    if (isPoW)
                    {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("Found Block !!!");
                        Console.WriteLine($"Block Hash: {blockHash}\n");

                        MinerToNode.PostRequestToNode(blockHash, timestamp, nonce);

                        blockFount = true;
                        break;
                    }

                    if (nonce % 100000 == 0)
                    {
                        timestamp = DateTime.UtcNow.ToString("o");
                    }

                    nonce++;

                    if (maxBlockTime < timer.Elapsed)
                    {
                        timer.Reset();
                        break;
                    }
                }
            }
        }


        public static byte[] Sha256(byte[] array)
        {
            SHA256Managed stringHash = new SHA256Managed();
            return stringHash.ComputeHash(array);
        }

        public static string BytesArrayToHexString(byte[] bytes)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            foreach (byte b in bytes)
            {
                result.Append(hexAlphabet[(int)(b >> 4)]);
                result.Append(hexAlphabet[(int)(b & 0x0F)]);
            }

            return result.ToString();
        }

        private static bool ProofOfWork(string precomputedData, BlockTemplate blockTemplate, string blockHash, string timestamp, long nonce)
        {
            int sum = 0;
            for (int i = 0; i < blockTemplate.Difficulty; i++)
            {
                sum += blockHash[i];
            }

            int expectedSum = 48 * blockTemplate.Difficulty;

            return sum == expectedSum;
        }

    }
}