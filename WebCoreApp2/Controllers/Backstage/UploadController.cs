using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;

namespace CCISWebCore.Controllers
{
    [Authorize(Roles = "Admin")]
    /// <summary>
    /// Upload 上傳控制層
    /// </summary>
    public class UploadController : Controller
    {
        private IHostingEnvironment _env;
        private readonly string _folder;
        public UploadController(IHostingEnvironment env, IConfiguration config)
        {
            _env = env;
            _folder = config["Data:FilePath:PhysicalPath"];
        }

        [HttpPost]
        [RequestSizeLimit(4000000)]
        [Authorize(Roles = "Admin")]
        public ActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;
            if (!IsImage(upload))
            {
                var NotImageMessage = "請選擇圖片";
                dynamic NotImage = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + NotImageMessage + "\"}}");
                return Json(NotImage);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

            List<string> _fs = new List<string>();
            _fs.Add(_folder);
            _fs.Add("Info");
            var _phyPath = Path.Combine(_fs.ToArray());

            if (!Directory.Exists(_phyPath))
            {
                Directory.CreateDirectory(_phyPath);
            }
            //組成 要上傳的路徑的檔案
            var path = Path.Combine(
                _phyPath,
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);
            }

            //對應靜態檔案實體路徑       
            var url = Url.Content("~/_FileUploads/Info/") + fileName;

            var successMessage = "圖片上傳成功";
            dynamic success = JsonConvert.DeserializeObject("{ 'uploaded': 1,'fileName': \"" + fileName + "\",'url': \"" + url + "\", 'error': { 'message': \"" + successMessage + "\"}}");
            return Json(success);
        }

        public bool IsImage(IFormFile file)
        {
            try
            {
                var img = Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}