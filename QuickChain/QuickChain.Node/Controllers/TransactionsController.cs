using Microsoft.AspNetCore.Mvc;
using QuickChain.Data;
using QuickChain.Model;
using QuickChain.Node.Model;
using QuickChain.Node.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IRepository<SignedTransaction> transactionsRepository;
        private readonly IHashLibrary hashLibrary;
        private readonly INextBlockComposer nextBlockComposer;

        public TransactionsController(IRepository<SignedTransaction> transactionsRepository, IHashLibrary hashLibrary, INextBlockComposer nextBlockComposer)
        {
            this.transactionsRepository = transactionsRepository;
            this.hashLibrary = hashLibrary;
            this.nextBlockComposer = nextBlockComposer;
        }

        [HttpGet]
        public IEnumerable<SignedTransaction> GetAll(bool? onlyConfirmed, bool? onlyPending, int? page, int? perPage, string address = null)
        {
            IQueryable<SignedTransaction> allTransactions = transactionsRepository.GetAll(false);

            if (address != null)
            {
                allTransactions = allTransactions.Where(t => t.From == address || t.To == address);
            }
            if (onlyConfirmed == true)
            {
                allTransactions = allTransactions.Where(t => t.BlockHeight >= 0);
            }
            if (onlyPending == true)
            {
                allTransactions = allTransactions.Where(t => t.BlockHeight < 0);
            }

            return allTransactions.Skip(((page ?? 1) - 1) * (perPage ?? 20))
                .Take(perPage ?? 20);
        }

        [HttpGet("{hash}")]
        public SignedTransaction Get(string hash)
        {
            return this.transactionsRepository.GetAll().Single(t => t.TransactionHash == hash);
        }

        [HttpPost()]
        public SignedTransaction Create([FromBody]TransactionModel transaction)
        {
            //if (this.hashLibrary.GetHash(transaction) != transaction.TransactionHash)
            //{
            //    throw new UnauthorizedAccessException("not valid hash");
            //}

            //if (!this.hashLibrary.IsValidSignature(transaction, transaction.SignatureR, transaction.SignatureS))
            //{
            //    throw new UnauthorizedAccessException("not valid signature");
            //}

            var newTransaction = new SignedTransaction()
            {
                BlockHeight = 0,
                CreatedOn = DateTime.UtcNow,
                TransactionIdentifier = transaction.TransactionIdentifier,
                Fee = transaction.Fee,
                Value = transaction.Value,
                From = transaction.From,
                To = transaction.To,
                SenderPublicKey = transaction.SenderPublicKey,
                SignatureR = transaction.SignatureR,
                SignatureS = transaction.SignatureS,
                TransactionHash = transaction.TransactionHash,
            };

            SignedTransaction dbTransaction = this.transactionsRepository.Insert(newTransaction);
            this.transactionsRepository.Save();

            this.nextBlockComposer.AddTransactionToNextBlock(dbTransaction);

            return dbTransaction;

        }

        //[HttpPost("{hash}/sign")]
        //public SignedTransaction Sign(string hash, [FromBody]string signatureR, [FromBody]string signatureS)
        //{
        //    SignedTransaction transaction = this.transactionsRepository.GetAll().Single(t => t.TxHash == hash);

        //    if (!this.hashLibrary.IsValidSignature(transaction, signatureR, signatureS))
        //    {
        //        throw new UnauthorizedAccessException("not valid signature");
        //    }

        //    this.transactionsRepository.Attach(transaction);
        //    transaction.SignatureR = signatureR;
        //    transaction.SignatureS = signatureS;
        //    transaction.LastEditedOn = DateTime.UtcNow;
        //    this.transactionsRepository.Save();

        //    return transaction;
        //}

        //[HttpPost("{hash}/send")]
        //public string Send(string hash)
        //{
        //    SignedTransaction transaction = this.transactionsRepository.GetAll().Single(t => t.TxHash == hash);

        //    this.nextBlockComposer.AddTransactionToNextBlock(transaction);

        //    return "The transaction has been included in the transaction pool! Wait for the transaction to be mined!";
        //}

        public IActionResult Faucet()
        {
            return View("~/Views/Blocks/Faucet.cshtml");
        }
        public string FaucetSend(string senderAddres)
        {
            const string privateKey = "c713a77220ccd22ab38dbbbd019ec07d23ba2361335ce56671b5fe7ce9901656";
            const string publicKey = "04948882b505099c9a801aabe4ae0a855fc26d1c968c100727148c666668f5fc1cc82f3a57c156d79c5c84c48fff0093eb66d42fe894e90e4c680984eb77938640";
            const string addres = "1bb72da277b3d2c95c8956db4f1f452e47e3409c";
            return "";
        }
    }
}
