using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickChain.Node.Controllers
{
    public class FaucetController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string address)
        {

            this.ViewBag.IsSend = true;
            return View("Index");
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
