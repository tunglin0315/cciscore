using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCISWebCore.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CCISWebCore.Models.ViewModels;
using Project.Common.Helper;
using System.IO;
using Microsoft.Extensions.Configuration;

//using CCISWebCore.Models;

namespace CCISWebCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly dbCCISCoreContext _context;
        private readonly IConfiguration _config;
        private readonly string _folder;

        private List<Banner> Banners
        {
            get
            {
                XSerializer<List<Banner>> BannerShow =
                    new XSerializer<List<Banner>>(FanlPath("BannerSet.xml"));
                List<Banner> banners = BannerShow.Deserialize();
                return banners ?? new List<Banner>();
            }
            set
            {
                XSerializer<List<Banner>> BannerShow =
                   new XSerializer<List<Banner>>(FanlPath("BannerSet.xml"));
                BannerShow.Serialize(value);
            }
        }
        private List<News> Newss
        {
            get
            {
                XSerializer<List<News>> NewsShow =
                    new XSerializer<List<News>>(FanlPath("NewsSet.xml"));
                List<News> newss = NewsShow.Deserialize();
                return newss ?? new List<News>();
            }
            set
            {
                XSerializer<List<News>> NewsShow =
                   new XSerializer<List<News>>(FanlPath("NewsSet.xml"));
                NewsShow.Serialize(value);
            }
        }

        public HomeController(dbCCISCoreContext context ,  IConfiguration config) {

            _context = context;
            _config = config;
            _folder = _config["Data:FilePath:PhysicalPath"];
        }
        //前端測試
        //public async Task<IActionResult>  Index()
        //{
        //    return View(await _context.HomeActivity.ToListAsync());
        //}
        public IActionResult Index()
        {
            ViewData["NewsList"] = this.Newss;
            return View(this.Banners);
        }
        //public ViewResult Demo() => View();

        public ViewResult Demo2() => View();

        public IActionResult Contact() => View();

        //2019/03/19 調整及新增的項目
        //2019/09/10 整理不必要的頁面
        //減碳政策 --
        //public IActionResult Policy()
        //{
        //    return View();
        //}
        //施政績效 --
        //public IActionResult Performance()
        //{
        //    return View();

        //}
        //六大部門溫管行動方案 --
        //public IActionResult Actionplan()
        //{
        //    return View();
        //}
        //產業溫室氣體盤查管理 --
        //public IActionResult GreenhouseGas()
        //{
        //    return View();
        //}
        //排放量申報暨盤查管理 --
        //public IActionResult Emission ()
        //{
        //    return View();
        //}
        //抵換專案 --
        //public IActionResult Exchange()
        //{
        //    return View();
        //}
        //永續家園 --
        //public IActionResult Perseverance()
        //{
        //    return View();
        //}
        // 綠色生活 --
        //public IActionResult Greenlife()
        //{
        //    return View();
            
        //}
        //地球日系列活動
        //public IActionResult Activity()
        //{
        //    return View();
        //}


        //站內google Search 
        public IActionResult Search() {

            return View();
        }

        //全網站error頁面
        [Route("/error")]
        public IActionResult Error()
        {
            return View();
        }

        [Route("/NotFound")]
        public IActionResult NotFoundPgae()
        {
            return View();
        }

        private string FanlPath(string fileName)
        {
            List<string> _fs = new List<string>();
            _fs.Add(_folder);
            _fs.Add("DocSet");

            var _phyPath = Path.Combine(_fs.ToArray());
            //無資料夾，就建立資料夾
            if (!Directory.Exists(_phyPath))
            {
                Directory.CreateDirectory(_phyPath);
            }

            _fs.Add(fileName);
            return Path.Combine(_fs.ToArray());
        }
    }
}