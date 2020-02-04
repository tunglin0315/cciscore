using System;
using System.Collections.Generic;
using System.Text;
using Project.Common.Filter;
using System.Threading.Tasks;
using System.IO;
//using Microsoft.AspNetCore.Http;

namespace Project.Common.FileUpload
{
    class FileUpload
    {

        //public async Task<bool> UploadFile (IFormFile file)
        //{
        //    try {
        //        bool isCopied = false;
                
        //        if (file.Length > 0)
        //        {
        //            string FileName = file.FileName;
        //            string Extension = Path.GetExtension(FileName);
        //            if (Extension == ".png" || Extension == ".jpg" || Extension == ".jpeg" || Extension == ".gif")
        //            {
        //                string FilePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "FileUploads"));
        //                using ( var fs = new FileStream(Path.Combine(FilePath, FileName), FileMode.Create))
        //                {
        //                    await file.CopyToAsync(fs);
        //                    isCopied = true;
        //                }

        //            }
        //            else
        //            {
        //                throw new Exception("上傳的檔案,副檔名不符合規格!!");

        //            }

        //        }
        //           return isCopied;
                

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }


        //}

    }
}
