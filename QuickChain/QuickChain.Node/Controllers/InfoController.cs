using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    [Route("[controller]")]
    public class InfoController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "value";
        }
    }
}
