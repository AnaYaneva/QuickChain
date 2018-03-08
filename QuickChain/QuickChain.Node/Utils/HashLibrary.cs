using Newtonsoft.Json;
using QuickChain.Model;
using QuickChain.Node.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickChain.Node.Utils
{
    public class HashLibrary : IHashLibrary
    {
        public string GetHash(TransactionModel transaction)
        {
            transaction.SignatureR = null;
            transaction.SignatureS = null;

            string jsonData = JsonConvert.SerializeObject(transaction);

            using (SHA256 hashFunction = SHA256.Create())
            {
                byte[] hash = hashFunction.ComputeHash(Encoding.ASCII.GetBytes(jsonData));

                return Convert.ToBase64String(hash);
            }
        }

        public string GetHash(Block block)
        {
            return "TODO";
            
        }

        public bool IsValidSignature(TransactionModel transaction, string r, string s)
        {
            // TODO: implement
            return true;
        }
    }
}
