using LiberacionProductoWeb.Models.Dummys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult PendingTask()
        {
            return View();
        }
    }
}
