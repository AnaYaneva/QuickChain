namespace QuickChain.Miner
{
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
    }
}