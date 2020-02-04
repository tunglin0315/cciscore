using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CCISWebCore.Controllers
{
    public class GuideController : Controller
    {
        public IActionResult Guide()
        {
            return View();
        }
    }
}