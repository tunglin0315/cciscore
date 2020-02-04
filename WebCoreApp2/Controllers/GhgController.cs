using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCISWebCore.Models.EF;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCISWebCore.Controllers
{
    public class GhgController : Controller
    {
        private readonly dbCCISCoreContext _context;
        public GhgController(dbCCISCoreContext context)
        {
            _context = context;
        }
        public IActionResult Index([FromRoute]string id)
        {
            ViewData["name"] = id;
            string _url =System.Net.WebUtility.UrlDecode(Url.Action().ToString());
            Infos infos = _context.Infos.FirstOrDefault(a => a.HtmlUrl == _url);
            if (infos == null)
            {
               return NotFound();
            }
            else
            {
                ViewBag.html = string.IsNullOrEmpty(infos.HtmlContent) ? "" : infos.HtmlContent;
                ViewBag.Title = string.IsNullOrEmpty(infos.Title) ? "" : infos.Title;
                ViewBag.id = infos.Id;
            }
            return View();
          
        }

    }
}
