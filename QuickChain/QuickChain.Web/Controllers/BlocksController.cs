using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuickChain.Model;

namespace QuickChain.Web.Controllers
{
    public class BlocksController : Controller
    {
        public static readonly List<Block> list = new List<Block>();

        public IActionResult Index()
        {
            var transaction = new SignedTransaction();
            transaction.BlockHeight = 1;
            transaction.From = "ME";
            transaction.To = "YOU";
            transaction.TxHash = "TRANSACTION-HASH";
            transaction.UsedGas = 10;
            transaction.Value = 10;

            var block = new Block();
            block.Height = 1;
            block.TimeStamp = DateTime.Now;
            block.Size = 15;
            block.Hash = "HASH";
            block.ParentHash = "PARENTHASH";
            block.Transactions.Add(transaction);
            list.Add(block);

            return View(list);
        }

        public IActionResult Block(int id)
        {
            return View(list.FirstOrDefault(b => b.Height == id));
        }
        public IActionResult Transactions(int blockId)
        {
            return View(list.FirstOrDefault(b => b.Height == 1).Transactions.ToList());
        }
        public IActionResult Transaction(string id)
        {
            foreach (var block in list)
            {
                foreach (var t in block.Transactions)
                {
                    if (t.TxHash == id)
                        return View(t);
                }
            }

            return View();
        }
        
        public IActionResult PendingTransactions(List<SignedTransaction> penndingList)
        {
            return View(penndingList);
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