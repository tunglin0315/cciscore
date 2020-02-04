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
    public class DocSetMovieController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly string _folder;
        //次目錄
        private readonly string _subFolder = "Movie";
        private List<Movie> Movies
        {
            get
            {
                XSerializer<List<Movie>> MovieShow =
                    new XSerializer<List<Movie>>(FanlPath());
                List<Movie> movies = MovieShow.Deserialize();
                return movies ?? new List<Movie>();
            }
            set
            {
                XSerializer<List<Movie>> MovieShow =
                   new XSerializer<List<Movie>>(FanlPath());
                MovieShow.Serialize(value);
            }
        }

        public DocSetMovieController(IHostingEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
            _folder = _config["Data:FilePath:PhysicalPath"];
        }

        public IActionResult MovieList()
        {
            List<Movie> itemMovies = (from items in this.Movies
                                      orderby items.Sort
                                    select items).ToList();
            this.Movies = itemMovies;
            return View(this.Movies);
        }

        public IActionResult Save([FromRoute]string Id = null)
        {
            Movie b = new Movie();
            bool IsAdd = Id == null;
            if (!IsAdd)
            {
                //編輯
                List<Movie> movies = this.Movies;
                var _d = movies.Where(w => w.Guid.Equals(Id));
                if (!_d.Any())
                {
                    return NotFound();
                }
                b = _d.First();
            }
            return View(b);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Movie b)
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

            List<Movie> movies = this.Movies;
            Movie _b;

            if (_isAdd)
            {
                _b = new Movie() { Guid = Guid.NewGuid().ToString() };
            }
            else
            {
                _b = movies.Where(w => w.Guid.Equals(b.Guid)).First();
                if (!movies.Remove(_b))
                {
                    RedirectToAction("MovieList");
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

            _b.Title = b.Title;
            _b.Alt = b.Alt;
            _b.H2 = b.H2;
            _b.Href = b.Href;
            _b.Sort = b.Sort;
            movies.Add(_b);
            List<Movie> itemMovies = (from items in movies
                                      orderby items.Sort
                                      select items).ToList();
            //存入Movies,並執行序列化(set)
            this.Movies = itemMovies;
            return RedirectToAction("MovieList");
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
            List<Movie> movies = this.Movies;
            Movie _b = movies.Where(w => w.Guid.Equals(Id)).First();
            DelFile(_b.ImgPath);
            if (movies.Remove(_b))
            {
                this.Movies = movies;
                //_meg = "刪除成功!";
            }
            else
            {
                //_meg = "刪除失敗!";
            }
            return RedirectToAction("MovieList");
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

            _fs.Add("MovieSet.xml");
            return Path.Combine(_fs.ToArray());
        }
    }
}