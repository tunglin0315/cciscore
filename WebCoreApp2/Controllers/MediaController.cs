using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCISWebCore.Models.EF;
using CCISWebCore.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Project.Common.Helper;
using System.IO;

namespace CCISWebCore.Controllers
{
    public class MediaController : Controller
    {
        private readonly dbCCISCoreContext _context;
        private readonly IConfiguration _config;
        private readonly string _folder;

        private List<Cartoon> Cartoons
        {
            get
            {
                XSerializer<List<Cartoon>> CartoonShow =
                    new XSerializer<List<Cartoon>>(FanlPath("CartoonSet.xml"));
                List<Cartoon> cartoons = CartoonShow.Deserialize();
                return cartoons ?? new List<Cartoon>();
            }
            set
            {
                XSerializer<List<Cartoon>> CartoonShow =
                   new XSerializer<List<Cartoon>>(FanlPath("CartoonSet.xml"));
                CartoonShow.Serialize(value);
            }
        }
        private List<Movie> Movies
        {
            get
            {
                XSerializer<List<Movie>> MovieShow =
                      new XSerializer<List<Movie>>(FanlPath("MovieSet.xml"));
                List<Movie> movies = MovieShow.Deserialize();
                return movies ?? new List<Movie>();
            }
            set {
                XSerializer<List<Movie>> MovieShow =
               new XSerializer<List<Movie>>(FanlPath("MovieSet.xml"));
                MovieShow.Serialize(value);
            }
        }
        private List<Poster> Posters
        {
            get
            {
                XSerializer<List<Poster>> PosterShow =
                      new XSerializer<List<Poster>>(FanlPath("PosterSet.xml"));
                List<Poster> posters = PosterShow.Deserialize();
                return posters ?? new List<Poster>();
            }
            set
            {
                XSerializer<List<Poster>> PosterShow =
               new XSerializer<List<Poster>>(FanlPath("PosterSet.xml"));
                PosterShow.Serialize(value);
            }
        }

        public MediaController(dbCCISCoreContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _folder = _config["Data:FilePath:PhysicalPath"];
        }

        public IActionResult Index()
        {
            return View();
        }
        //碳足跡計算器
        public IActionResult CarbonTool([FromRoute]string id)
        {
            ViewData["number"] =
                string.IsNullOrEmpty(id) ? "" : id;
            return View();
        }

        public ActionResult Exp()
        {
            return View();
        }
        //相關影音
        public ActionResult Movie()
        {
            return View(Movies);
        }
        //桌布下載
        public ActionResult DownLoad()
        {
            return View();
        }
        //氣候變遷漫畫
      
        public IActionResult Cartoon(int? id)
        {
            //id無值預設為1
            int _no = id ?? 1;
            //要顯示的順位大於總數量則轉到沒有發現網頁
            if (this.Cartoons.Count < _no) { return NotFound(); }

            ViewData["CartoonNo"] = _no;
            return View(this.Cartoons);
        }
        //秒懂圖
        public IActionResult Second()
        {
            return View();
        }

        //宣傳海報 index
        public IActionResult Poster()
        {
            return View(Posters);
        }
        //宣傳海報 低碳生活
        public IActionResult Poster2()
        {
            List<Poster> itemPosters = (from items in this.Posters
                                        where items.IsLow == true
                                        select items).ToList();
            return View(itemPosters);
        }
        //宣傳海報 住商部門
        public IActionResult Poster3()
        {
            List<Poster> itemPosters = (from items in this.Posters
                                        where items.IsLiving.Equals(true)
                                        select items).ToList();
            return View(itemPosters);
        }
        //宣傳海報 辦公相關
        public IActionResult Poster4()
        {
            List<Poster> itemPosters = (from items in this.Posters
                                        where items.IsOffice.Equals(true)
                                        select items).ToList();
            return View(itemPosters);
        }

        //宣傳資源 4個子項選單
        public IActionResult Detail_Sub()
        {
            return View();

        }

        //宣傳資源_檔案下載 2個孫項選單
        public IActionResult Detail_Sub_Sub()
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
            fileName = fileName.Trim();
            _fs.Add(fileName);
            return Path.Combine(_fs.ToArray());
        }
    }
}