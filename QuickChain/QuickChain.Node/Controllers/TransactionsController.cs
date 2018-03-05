using Microsoft.AspNetCore.Mvc;
using QuickChain.Data;
using QuickChain.Model;
using QuickChain.Node.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private readonly IRepository<Transaction> transactionsRepository;

        public TransactionsController(IRepository<Transaction> transactionsRepository)
        {
            this.transactionsRepository = transactionsRepository;
        }

        [HttpGet]
        public IEnumerable<Transaction> GetAll(bool? onlyConfirmed, bool? onlyPending, int? page, int? perPage, string address = null)
        {
            IQueryable<Transaction> allTransactions = transactionsRepository.GetAll(false);

            if (address != null)
            {
                allTransactions = allTransactions.Where(t => t.From == address || t.To == address);
            }
            if(onlyConfirmed == true)
            {
                allTransactions = allTransactions.Where(t => t.BlockHeight >= 0);
            }
            if(onlyPending == true)
            {
                allTransactions = allTransactions.Where(t => t.BlockHeight < 0);
            }

            return allTransactions.Skip(((page ?? 1) - 1) * (perPage ?? 20))
                .Take(perPage ?? 20);
        }

        [HttpGet("{hash}")]
        public Transaction Get(string hash)
        {
            return this.transactionsRepository.GetAll().Single(t => t.TxHash == hash);
        }

        [HttpPost()]
        public Transaction Create([FromBody]Transaction transaction)
        {
            Transaction dbTransaction = this.transactionsRepository.Insert(transaction);
            this.transactionsRepository.Save();

            return dbTransaction;

        }

        [HttpPost("{hash}/sign")]
        public Transaction Sign(string hash, [FromBody]Signature signature)
        {
            return new Transaction();
        }

        [HttpPost("{hash}/send")]
        public Transaction Send(string hash)
        {
            return new Transaction();
        }
    }
}
