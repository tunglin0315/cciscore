using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCISWebCore.Models.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CCISWebCore.Controllers
{
    public class KnowController : Controller
    {

        private readonly dbCCISCoreContext _context;
        public KnowController(dbCCISCoreContext context)
        {
            _context = context;
        }
        //public IActionResult Index([FromRoute]string id)
        //{
        //    ViewData["name"] = id;
        //    return View();
        //}

        //氣候變遷知識 3各子項選單
        public IActionResult Detail_Sub()
        {
            return View();
        }
        //氣候變遷介紹 4個孫選單
        public IActionResult Detail_Sub_Sub()
        {
            return View();
        }
        //氣候公約介紹 3個孫選單
        public IActionResult Detail_Sub_Sub2()
        {
            return View();
        }
        //各國氣候變遷調適 
        public IActionResult EachCountry()
        {
            return View();
        }
        //歐盟
        public IActionResult EuropeanUnion()
        {
            return View();
        }
        //英國
        public IActionResult English()
        {
            return View();
        }
        //德國
        public IActionResult Germany()
        {
            return View();
        }
        //法國
        public IActionResult France()
        {
            return View();
        }
        //冰島
        public IActionResult Iceland()
        {
            return View();
        }
        //新加坡
        public IActionResult Singapore()
        {
            return View();
        }
        //日本
        public IActionResult Japan()
        {
            return View();
        }
        //南韓
        public IActionResult Korea()
        {
            return View();
        }
        //澳洲
        public IActionResult Australia()
        {
            return View();
        }
        //荷蘭
        public IActionResult Netherlands()
        {
            return View();
        }
        //加拿大
        public IActionResult Canada()
        {
            return View();
        }
        //美國
        public IActionResult America()
        {
            return View();
        }
        //丹麥
        public IActionResult Denmark()
        {
            return View();
        }

        //溫室效應與全球暖化
        public IActionResult Detail()
        {
            //取路徑字串
            string _url = Url.Action().ToString();
            Infos infos = _context.Infos.FirstOrDefault(a => a.HtmlUrl == _url);
            if (infos == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.Html = string.IsNullOrEmpty(infos.HtmlContent) ? "" : infos.HtmlContent;
                ViewBag.Title = string.IsNullOrEmpty(infos.Title) ? "" : infos.Title;
                ViewBag.id = infos.Id;
            }
        
            return View();
        }
        
        //氣候變遷衝擊影響
        public IActionResult Detail2()
        {
            string _url = Url.Action().ToString();
            Infos infos = _context.Infos.FirstOrDefault(a => a.HtmlUrl == _url);

            if (infos == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.Html = string.IsNullOrEmpty(infos.HtmlContent) ? "" : infos.HtmlContent;
                ViewBag.Title = string.IsNullOrEmpty(infos.Title) ? "" : infos.Title;
                ViewBag.id = infos.Id;
            }

            return View();
        }

        //氣候變遷減緩
        public IActionResult Detail3()
        {
            string _url = Url.Action().ToString();
            Infos infos = _context.Infos.FirstOrDefault(a => a.HtmlUrl == _url);
            if (infos == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.Html = string.IsNullOrEmpty(infos.HtmlContent) ? "" : infos.HtmlContent;
                ViewBag.Title = string.IsNullOrEmpty(infos.Title) ? "" : infos.Title;
                ViewBag.id = infos.Id;
            }
            return View();
        }
        ////我們可以做什麼
        //public IActionResult Detail3_1()
        //{
        //    return View();
        //}
        ////社區可以什麼
        //public IActionResult Detail3_2()
        //{
        //    return View();
        //}
        //企業可以做什麼
        //public IActionResult Detail3_3()
        //{
        //    return View();
        //}

        //氣候變遷調適
        public IActionResult Detail4()
        {
            string _url = Url.Action().ToString();
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

        ////宣傳海報 index
        //public IActionResult Poster()
        //{
        //    return View();
        //}
        ////宣傳海報 低碳生活
        //public IActionResult Poster2()
        //{
        //    return View();
        //}
        ////宣傳海報 住商部門
        //public IActionResult Poster3()
        //{
        //    return View();
        //}
        ////宣傳海報 辦公相關
        //public IActionResult Poster4()
        //{
        //    return View();
        //}
        //聯合國氣候變化綱要公約
        public IActionResult Pact()
        {
            string _url = Url.Action().ToString();
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
        //京都議定書
        public IActionResult Pact1()
        {
            string _url = Url.Action().ToString();
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
        //巴黎協定
        public IActionResult Pact2()
        {
            string _url = Url.Action().ToString();
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
