using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Project.Common.Helper
{
    /// <summary>
    /// 檔案處理
    /// </summary>
    public class FileHelper
    {
        #region 刪除檔案

        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="FileName">輸入要刪除的檔案名稱包含路徑</param>
        /// <returns>刪除成功回傳True , 刪除失敗回傳False</returns>
        public static bool FileDelete(string FileName)
        {
            FileInfo file1 = new FileInfo(FileName);

            if (file1.Exists == true) //檔案存在才進行刪除動作
            {
                file1.Delete();
                return true;
            }
            else
            {
                return false;//表示檔案不存在,無法進行檔案刪除動作
            }

        }

        #endregion


    }
}
