namespace QuickChain.Miner
{
    using Infrastructure;
    using Newtonsoft.Json;
    using QuickChain.Model;
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using System.Text;

    class Miner
    {
        const string hexAlphabet = "0123456789ABCDEF";

        static void Main(string[] args)
        {
            while (true)
            {
                string responseFromNode = MinerToNode.GetRequestToNode();

                MiningJob miningJob = JsonConvert.DeserializeObject<MiningJob>(responseFromNode);

                Console.WriteLine("\nStart new task:");
                Console.WriteLine($"Index of block to mine: {miningJob.Index}");
                // Console.WriteLine($"Expected Reward: {blockTemplate.ExpectedReward}");
                Console.WriteLine($"TransactionsHash: { miningJob.DataHash}");
                // Console.WriteLine($"PrevBlockHash: {blockTemplate.PrevBlockHash}");
                Console.WriteLine($"Difficulty: {miningJob.Difficulty}\n");


                for (long nonce = miningJob.NonceFrom; nonce <= miningJob.NonceTo; nonce++)
                {
                    string data = miningJob.DataHash + nonce;
                    // string blockHash = BytesArrayToHexString(Sha256(Encoding.UTF8.GetBytes(data)));

                    string blockHash = GetHash(data);

                    bool isPoW = ProofOfWork(miningJob, blockHash);
                    if (isPoW)
                    {
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("Found Block !!!");
                        Console.WriteLine($"Block Hash: {blockHash}\n");

                        MinerToNode.PostRequestToNode(nonce, miningJob);
                        break;
                    }
                }
            }
        }

        public static string GetHash(string data)
        {
            using (SHA256 hashFunction = SHA256.Create())
            {
                byte[] hash = hashFunction.ComputeHash(Encoding.ASCII.GetBytes(data));

                return Convert.ToBase64String(hash);
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

        private static bool ProofOfWork(MiningJob miningJob, string blockHash)
        {
            //int sum = 0;
            //for (int i = 0; i < miningJob.Difficulty; i++)
            //{
            //    sum += blockHash[i];
            //}

            //int expectedSum = 48 * miningJob.Difficulty;

            //return sum == expectedSum;

            return blockHash.StartsWith(new string('0', miningJob.Difficulty));
        }

    }
}