using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuickChain.Data;
using QuickChain.Model;

namespace QuickChain.Web.Controllers
{
    public class BlocksController : Controller
    {
        private readonly IRepository<Block> blockRepository;
        private readonly IRepository<SignedTransaction> transactionsRepository;

        public BlocksController(IRepository<Block> blockRepository, IRepository<SignedTransaction> transactionsRepository)
        {
            this.blockRepository = blockRepository;
            this.transactionsRepository = transactionsRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Block> blocks = this.blockRepository.GetAll();

            return View(blocks);
        }

        public IActionResult Block(int id)
        {
            Block block = this.blockRepository.GetByIdIncluding(id, b => b.Transactions);

            return View(block);
        }

        public IActionResult Transactions(int id)
        {
            Block block = this.blockRepository.GetByIdIncluding(id, b => b.Transactions);

            return View(block.Transactions);
        }

        public IActionResult Transaction(string id)
        {
            SignedTransaction transaction = this.transactionsRepository.GetAll()
                .FirstOrDefault(t => t.TransactionHash == id.ToLower());

            return View(transaction);
        }

        public IActionResult PendingTransactions(List<Transaction> penndingList)
        {
            IEnumerable<SignedTransaction> transactions = this.transactionsRepository.GetAll()
                .Where(t => t.BlockHeight == 0);

            return View(transactions);
        }

        public IActionResult PendingTransaction(string id)
        {
            return View(/*PendingList.where id == id*/);
        }

        public IActionResult Faucet()
        {
            return View();
        }
    }
}