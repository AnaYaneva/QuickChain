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
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IRepository<SignedTransaction> transactionsRepository;
        private readonly IRepository<Block> blocksRepository;

        public AddressController(IRepository<SignedTransaction> transactionsRepository, IRepository<Block> blocksRepository)
        {
            this.transactionsRepository = transactionsRepository;
            this.blocksRepository = blocksRepository;
        }

        [HttpGet("{address}/balance")]
        public BalanceModel GetBalance(string address)
        {
            // TODO read the values properly
            int confirmationsNeeded = 10;
            int currentBlockHeight = this.blocksRepository.GetAll(false).Last().Height;

            decimal confirmedBalance = this.GetBalanceTo(address, 0, currentBlockHeight - confirmationsNeeded) - this.GetBalanceFrom(address, 0, currentBlockHeight - confirmationsNeeded);
            decimal lastBalance = this.GetBalanceTo(address, currentBlockHeight - confirmationsNeeded, currentBlockHeight) - this.GetBalanceFrom(address, currentBlockHeight - confirmationsNeeded, currentBlockHeight);
            decimal pendingBalance = this.GetBalanceTo(address, -10, 0);

            var result = new BalanceModel()
            {
                Address = address,
                ConfirmedBalance = new BalanceInfo()
                {
                    Balance = confirmedBalance,
                    Confirmations = confirmationsNeeded,
                },
                LastMinedBalance = new BalanceInfo()
                {
                    Balance = confirmedBalance + lastBalance,
                    Confirmations = 1,
                },
                PendingBalance = new BalanceInfo()
                {
                    Balance = confirmedBalance + lastBalance + pendingBalance,
                    Confirmations = 0,
                },
            };

            return result;
        }

        private decimal GetBalanceFrom(string address, int blockHeightFrom, int blockHeightTo)
        {
            return this.transactionsRepository
                .GetAll()
                .Where(t => t.From == address && (blockHeightFrom < t.BlockHeight && t.BlockHeight <= blockHeightTo))
                .Sum(x => x.Value);
        }

        private decimal GetBalanceTo(string address, int blockHeightFrom, int blockHeightTo)
        {
            return this.transactionsRepository
                .GetAll()
                .Where(t => t.To == address && (blockHeightFrom < t.BlockHeight && t.BlockHeight <= blockHeightTo))
                .Sum(x => x.Value);
        }

        [HttpGet("{address}/transactions")]
        public IEnumerable<Transaction> GetTransactions(string address, int? page, int? perPage)
        {
            return this.transactionsRepository
                .GetAll(false)
                .Where(t => t.From == address || t.To == address)
                .Skip(((page ?? 1) - 1) * (perPage ?? 20))
                .Take(perPage ?? 20);
        }
    }
}
