using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Project.Common.Filter
{
    /// <summary>
    /// 共用字元過濾
    /// </summary>
    public class ExtensionsFilter
    {
        #region <副檔名過濾>
        /// <summary>
        ///  副檔名過濾
        /// </summary>
        public static Boolean ExtFilter(string fileExtension)
        {
            Boolean FileChk = false;            
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif" };
            for (int i = 0; i <= allowedExtensions.Length - 1; i++)
            {
                if (fileExtension == allowedExtensions[i])
                    FileChk = true;
            }
            return FileChk;
        }

        #endregion
    }
}
