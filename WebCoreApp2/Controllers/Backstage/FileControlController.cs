using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace CCISWebCore.Controllers.Backstage
{
    [Authorize(Roles = "Admin")]
    public class FileControlController : Controller
    {
        //private readonly static Dictionary<string, string> _contentTypes = new Dictionary<string, string>
        //{
        //    {".png", "image/png"},
        //    {".jpg", "image/jpeg"},
        //    {".jpeg", "image/jpeg"},
        //    {".gif", "image/gif"}
        //};
        private readonly string _folder;
        private readonly IConfiguration _config;

        public FileControlController(IHostingEnvironment env, IConfiguration config)
        {
            _config = config;
            //_folder = $@"{env.WebRootPath}\FileUploads";
            _folder = _config["Data:FilePath:PhysicalPath"];
        }

        public async Task<IActionResult> Upload(IFormFile files,string[] subFolder = null)
        {           
            if (files.Length > 0)
            {
                List<string> _fs = new List<string>();
                _fs.Add(_folder);
                if (subFolder != null)
                {
                    _fs = 
                        _fs.Concat(subFolder.ToList())
                        .ToList();
                }

                var _phyPath = Path.Combine(_fs.ToArray());
                //沒有資料夾
                if (!Directory.Exists(_phyPath))
                {
                    //建立資料夾
                    Directory.CreateDirectory(_phyPath);
                }

                _fs.Add(files.FileName);
                string path = Path.Combine(_fs.ToArray());

                //var path = $@"{_folder}\{subFolder}\{files.FileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
            }

            //return Ok(new { count = files.Count, size });
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}