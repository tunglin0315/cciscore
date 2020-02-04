using CCISWebCore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CCISWebCore.Controllers.Backstage
{
    [Authorize(Roles = "Admin")]
    public class DocSetController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly string _folder;
        //次目錄
        private readonly string _subFolder = "Banner";

        private List<Banner> Banners
        {
            get
            {
                XSerializer<List<Banner>> BannerShow =
                    new XSerializer<List<Banner>>(FanlPath());
                List<Banner> banners = BannerShow.Deserialize();
                return banners ?? new List<Banner>();
            }
            set
            {
                XSerializer<List<Banner>> BannerShow =
                   new XSerializer<List<Banner>>(FanlPath());
                BannerShow.Serialize(value);
            }
        }

        public DocSetController(IHostingEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
            _folder = _config["Data:FilePath:PhysicalPath"];
        }

        public IActionResult BannerList()
        {
            List<Banner> itemBanners = (from items in this.Banners
                                    orderby items.Sort
                                    select items).ToList();
            this.Banners = itemBanners;
            return View(this.Banners);
        }

        public IActionResult Save([FromRoute]string Id = null)
        {
            Banner b = new Banner();
            bool IsAdd = Id == null;
            if (!IsAdd)
            {
                //編輯
                List<Banner> banners = this.Banners;
                var _d = banners.Where(w => w.Guid.Equals(Id));
                if (!_d.Any())
                {
                    return NotFound();
                }
                b = _d.First();
            }
            return View(b);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Banner b)
        {
            //是否為新增
            bool _isAdd = b.Guid == null;
            //是否沒選擇圖片上傳
            bool _noFile = b.File == null;

            //驗證:新增一定要有上傳檔案
            if (_isAdd && _noFile) { ModelState.AddModelError("File", "請選擇上傳圖片"); }
            //有檔案，圖片上傳驗證
            if (!_noFile)
            {
                string[] _exs = { ".png", ".jpg", ".jpeg", ".gif" };
                string Extension = Path.GetExtension(b.File.FileName).ToLower();
                //不符合副檔名，返迴原資料
                if (!_exs.Contains(Extension))
                {
                    ModelState.AddModelError("File", "上傳圖片格式限" + string.Join("、", _exs));
                }
            }
            if (!ModelState.IsValid) { return View(b); }

            List<Banner> banners = this.Banners;
            Banner _b;

            if (_isAdd)
            {
                _b = new Banner() { Guid = Guid.NewGuid().ToString() };
            }
            else
            {
                _b = banners.Where(w => w.Guid.Equals(b.Guid)).First();
                if (!banners.Remove(_b))
                {
                    RedirectToAction("BannerList");
                }
            }

            //有圖片
            if (!_noFile)
            {
                //編輯
                if (!_isAdd)
                {
                    DelFile(_b.ImgPath);
                }
                //上傳圖片
                await new FileControlController(_env, _config)
                        .Upload(b.File, new string[] { _subFolder });
                //更新圖檔路徑
                _b.ImgPath = "~/_FileUploads/" + _subFolder + "/" + b.File.FileName;
            }

            _b.Href = b.Href;
            _b.Title = b.Title;
            _b.H2 = b.H2;
            _b.IsBlank = b.IsBlank;
            _b.Sort = b.Sort;
            banners.Add(_b);
            List<Banner> itemBanners = (from items in banners
                                        orderby items.Sort
                                        select items).ToList();
            //this.Banners = banners;
            this.Banners = itemBanners;
            return RedirectToAction("BannerList");
        }

        private void DelFile(string _delPath)
        {
            //取物理路徑字串
            var _folder = _config["Data:FilePath:PhysicalPath"];
            //取出檔案名
            var delstr = Path.GetFileName(_delPath);
            //組成完整路徑 
            var del_imgurl = $@"{_folder}\{_subFolder}\{delstr}";
            //刪除原本對應的檔案
            FileHelper.FileDelete(del_imgurl);
        }

        public IActionResult Delete([FromRoute]string Id)
        {
            List<Banner> banners = this.Banners;
            Banner _b = banners.Where(w => w.Guid.Equals(Id)).First();
            DelFile(_b.ImgPath);
            if (banners.Remove(_b))
            {
                this.Banners = banners;
                //_meg = "刪除成功!";
            }
            else
            {
                //_meg = "刪除失敗!";
            }
            return RedirectToAction("BannerList");
        }

        public string FanlPath()
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

            _fs.Add("BannerSet.xml");
            return Path.Combine(_fs.ToArray());
        }
    }
}