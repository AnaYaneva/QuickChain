using Microsoft.AspNetCore.Mvc;
using QuickChain.Model;
using QuickChain.Node.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    [Route("[controller]")]
    public class AddressController : Controller
    {
        [HttpGet("{address}/balance")]
        public BalanceModel GetBalance(string address)
        {
            return new BalanceModel();
        }

        [HttpGet("{address}/transactions")]
        public IEnumerable<Transaction> GetTransactions(string address)
        {
            return new Transaction[] { };
        }
    }
}
