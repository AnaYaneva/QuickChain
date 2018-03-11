using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Security.Cryptography;
using QuickChain.Node.Model;
using QuickChain.Node.Utils;
using System.Text;
using System.Linq;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Asn1.Nist;

namespace QuickChain.Node.Controllers
{
    public class FaucetController : Controller
    {
        private const string FaucetPrivateKey = "c713a77220ccd22ab38dbbbd019ec07d23ba2361335ce56671b5fe7ce9901656";
        private const string FaucetPublicKey = "04948882b505099c9a801aabe4ae0a855fc26d1c968c100727148c666668f5fc1cc82f3a57c156d79c5c84c48fff0093eb66d42fe894e90e4c680984eb77938640";
        private const string FaucetAddress = "1bb72da277b3d2c95c8956db4f1f452e47e3409c";
        private const string FaucetMnemonic = "shock crush better hunt stadium later mutual silly tragic consider journey target";

        private HashLibrary hashLibrary = new HashLibrary();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string address)
        {
            FaucetSend(address);
            return View("Index");
        }
        public string FaucetSend(string receiverAddress)
        {


            TransactionModel tr = new TransactionModel();
            tr.Fee = 0;
            tr.From = FaucetAddress;
            tr.SenderPublicKey = FaucetPublicKey;
            tr.To = receiverAddress;
            tr.Value = 1;
            tr.Time = DateTime.UtcNow;

            var hash = hashLibrary.GetHash(tr);
            byte[] tranhash = Convert.FromBase64String(hash);

            BigInteger[] tranSignature = SignData(new BigInteger(FaucetPrivateKey), tranhash);

            tr.TransactionHash = hash;
            var transactionHash = hashLibrary.GetHash(tr);
            signTransaction(transactionHash, FaucetPrivateKey);

            return "";
        }

        private static void signTransaction(string Hash, string prk)
        {
            byte[] hash = Convert.FromBase64String(Hash);
            byte[] privateK = Convert.FromBase64String(prk);

        }
        private BigInteger[] SignData(BigInteger privateKey, byte[] data)
        {
            // TODO: put curve name
            string curveName = "";
            var curve = NistNamedCurves.GetByName(curveName);
            ECDomainParameters ecSpec = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
            ECPrivateKeyParameters keyParameters = new ECPrivateKeyParameters(privateKey, ecSpec);
            IDsaKCalculator kCalculator = new HMacDsaKCalculator(new Sha256Digest());
            ECDsaSigner signer = new ECDsaSigner(kCalculator);
            signer.Init(true, keyParameters);
            BigInteger[] signature = signer.GenerateSignature(data);
            return signature;
        }

    }
}
