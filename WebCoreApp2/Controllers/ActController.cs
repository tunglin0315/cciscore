using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CCISWebCore.Models.EF;

namespace CCISWebCore.Controllers
{
    public class ActController : Controller
    {
        private readonly dbCCISCoreContext _context;
        public ActController(dbCCISCoreContext context)
        {
            _context = context;
        }

        public IActionResult Index([FromRoute]string id)
        {
            ViewData["name"] = id;

            string _url = System.Net.WebUtility.UrlDecode(Url.Action().ToString());
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
        //環保旅店
        public IActionResult Hotel()
        {

           return RedirectToAction("Detail_Sub_Sub", "act", null);
      
            //return View();
        }
        //綠色採購
        public IActionResult GreenShop()
        {
            return RedirectToAction("Detail_Sub_Sub", "act", null);

           // return View();

        }
        //減碳十大撇步
        public IActionResult Ten()
        {
           return View();
        }

        //低碳飲食
         public IActionResult Diet()
        {

            //取路徑字串
            string _url = Url.Action().ToString();
            GetHtmlInfo(_url);
            return View();

        }
        //縣市評比
        public IActionResult Evaluation()
        {
            return View();

        }

        //0418 新增搬移home control 裡的 view

        //溫室氣體減量與管理法
        //public IActionResult Policy()
        //{
        //    return View();
        //}
        //0627 捨棄該項目
        //六大部門溫管行動方案
        //public IActionResult Actionplan()
        //{
        //    return View();
        //}
        //國家因應氣候變遷行動綱領
        //public IActionResult NationalOutline()
        //{
        //    return View();
        //}
        //溫室氣體減量推動方案
        //public IActionResult PromotionPlan()
        //{
        //    return View();
        //}
        //部門溫室氣體排放管制行動方案
        //public IActionResult ControlPlan()
        //{
        //    return View();
        //}
        //溫室氣體管制執行方案
        //public IActionResult ExecutionPlan()
        //{
        //    return View();
        //}
        //產業溫室氣體盤查管理
        public IActionResult GreenhouseGas()
        {  //取路徑字串
            string _url = Url.Action().ToString();
            GetHtmlInfo(_url);
            return View();
        }
        //排放量申報暨盤查管理
        //public IActionResult Emission()
        //{
        //    //取路徑字串
        //    string _url = Url.Action().ToString();
        //    GetHtmlInfo(_url);
        //    return View();
        //}
        //抵換專案
        public IActionResult Exchange()
        {
            //取路徑字串
            string _url = Url.Action().ToString();
            GetHtmlInfo(_url);
            return View();
        }

        //調適領域與工作架構
        //public IActionResult Architecture()
        //{
        //    //取路徑字串
        //    string  _url = Url.Action().ToString();
        //    GetHtmlInfo(_url);
        //    return View();
        //}
    

        //推動機制與行動方案
        //public IActionResult Promote()
        //{ 
        //    //取路徑字串
        //    string _url = Url.Action().ToString();
        //    GetHtmlInfo(_url);
        //    return View();
        //}
        //總體調適計畫
        //public IActionResult Overall()
        //{ 
        //    //取路徑字串
        //    string _url = Url.Action().ToString();
        //    GetHtmlInfo(_url);
        //    return View();

        //}

        //國家氣候變遷調適行動方案
        public IActionResult NationalPlan()
        {
            //取路徑字串
            string _url = Url.Action().ToString();
            GetHtmlInfo(_url);
            return View();

        }
        // 熱浪來臨時
        public IActionResult HeatWave()
        {
            //取路徑字串
            string _url = Url.Action().ToString();
            GetHtmlInfo(_url);
            return View();
        }
        //低碳永續家園
        public IActionResult LowCarbon()
        {
            return View();
        }

        // 施政績效 20190627 捨棄該項目
        //public IActionResult Performance()
        //{

        //    return View();
        //}
        //因應作為 4個子選單
        public IActionResult Detail_Sub()
        {
            return View();
        }
        //氣候變遷全民行動 3個孫選單
         public IActionResult Detail_Sub_Sub()
        {
            return View();
        }
        /// <summary>
        /// 撈取後台 html 欄位資料
        /// </summary>
        /// <param name="_Url"></param>
        /// <returns></returns>
        public dynamic GetHtmlInfo(string _Url)
        {

            Infos infos = _context.Infos.FirstOrDefault(a => a.HtmlUrl == _Url);
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
            return true;
        }
    }
}
