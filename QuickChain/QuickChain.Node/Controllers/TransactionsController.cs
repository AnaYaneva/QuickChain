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
    public class TransactionsController : Controller
    {
        [HttpGet]
        public IEnumerable<Transaction> GetAll(string address = null, bool onlyConfirmed = false, bool onlyPending = false)
        {
            return new Transaction[] { };
        }

        [HttpGet("{id}")]
        public Transaction Get(string id)
        {
            return new Transaction();
        }

        [HttpPost()]
        public Transaction Create([FromBody]Transaction transaction)
        {
            return transaction;
        }

        [HttpPost("{id}/sign")]
        public Transaction Sign(string id, [FromBody]Signature signature)
        {
            return new Transaction();
        }

        [HttpPost("{id}/send")]
        public Transaction Send(string id)
        {
            return new Transaction();
        }
    }
}
