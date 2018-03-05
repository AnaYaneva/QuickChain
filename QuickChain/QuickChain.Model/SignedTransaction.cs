using System;
using System.Collections.Generic;
using System.Text;

namespace QuickChain.Model
{
    public class SignedTransaction : Transaction
    {
        public string SignatureR { get; set; }
        public string SignatureS { get; set; }
        public int BlockHeight { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
