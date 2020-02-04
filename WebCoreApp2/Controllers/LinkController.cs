using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCISWebCore.Controllers
{
    public class LinkController : Controller
    {
        public IActionResult Index([FromRoute]string id)
        {
            ViewData["name"] = id;
            return View();
        }

        //相關連結 2個子項 選單
        public IActionResult Detail_Sub()
        {
            return View();
        }
        //低碳永續家園
        public IActionResult Perseverance()
        {
            return View();

        }
        //綠色生活資訊網
        public IActionResult Greenlife()
        {
            return View();
        }
    }
}
