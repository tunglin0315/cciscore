using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCISWebCore.Controllers
{
    public class NewsController : Controller
    {       
        public IActionResult Index([FromRoute]string id)
        {          
            ViewData["name"] = id;
            return View();
        }

        
        public IActionResult DomesticNews ()
        {
            return View();
        }

        public IActionResult DomesticNewsList1()
        {

            return View();
        }
        public IActionResult DomesticNewsList2()
        {

            return View();
        }
        public IActionResult DomesticNewsList3()
        {

            return View();
        }

        public IActionResult DomesticNewsList4()
        {

            return View();
        }
        public IActionResult DomesticNewsList5()
        {

            return View();
        }




        public IActionResult NationalNews()
        {
            return View();
        }



        public IActionResult Detail() => View();

    }
}
