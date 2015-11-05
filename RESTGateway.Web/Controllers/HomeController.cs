using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.Framework.OptionsModel;
using RESTGateway.Core;

namespace RESTGateway.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IOptions<AppSettings> optionsAccessor)
        {
            Settings = optionsAccessor.Value;
        }

        AppSettings Settings { get; }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }
    }
}
