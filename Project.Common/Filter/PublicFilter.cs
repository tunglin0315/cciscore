using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Project.Common.Filter
{
    /// <summary>
    /// 共用字元過濾
    /// </summary>
    public class PublicFilter
    {
        #region <隱碼攻擊參數過濾>
        /// <summary>
        ///  SQL Injection Filter
        /// </summary>
        /// <param name="SQLValue">SQL參數</param>
        /// <returns>不含隱碼攻擊參數</returns>
        public static string ValueFilter(string SQLValue)
        {
            if (SQLValue != null)
            {
                SQLValue = Regex.Replace(SQLValue, @"\b(exec(ute)?|select|update|insert|delete|drop|create)\b|[;']|(-{2})|(/\*.*\*/)", string.Empty, RegexOptions.IgnoreCase);
            }
            else
            {
                SQLValue = "";
            }
            return SQLValue;
        }

        #endregion

        //#region <是否包含字元>
        ///// <summary>
        ///// 是否包含字元 (bool anyLuck = s.ContainsAny("a", "b", "c");)
        ///// </summary>
        ///// <param name="haystack"></param>
        ///// <param name="needles"></param>
        ///// <returns></returns>
        //public static bool ContainsAny(string haystack, params string[] needles)
        //{
        //    foreach (string needle in needles)
        //    {
        //        if (haystack.Contains(needle))
        //            return true;
        //    }

        //    return false;
        //}
        //#endregion

        #region <檢驗公司統一編號是否正確>
        /// <remarks>檢驗公司統一編號是否正確</remarks>
        public static bool checkCompanyNo(string arg_CompanyNo)
        {
            var LOGIC = new[] { 1, 2, 1, 2, 1, 2, 4, 1 };
            var intSum = 0;

            for (var i = 0; i < LOGIC.Length; i++)
            {
                var intMultiply = int.Parse(arg_CompanyNo.Substring(i, 1)) * LOGIC[i];
                var intAddition = ((intMultiply / 10) + (intMultiply % 10));
                intSum += (intAddition == 10) ? 1 : intAddition;
            }

            return (intSum % 10 == 0);
        }
        #endregion

        #region <檢查身分證字號>
        /// <remarks>檢查身分證字號</remarks>
        public bool check_twid(string iptID)
        {
            iptID = iptID.ToUpper();

            //檢查身分證字號
            string head = "ABCDEFGHJKLMNPQRSTUVXYWZIO";
            iptID = (head.IndexOf(iptID.Substring(0, 1)) + 10) + iptID.Substring(1, 9);
            int s = int.Parse(iptID.Substring(0, 1)) +
                int.Parse(iptID.Substring(1, 1)) * 9 +
                int.Parse(iptID.Substring(2, 1)) * 8 +
                int.Parse(iptID.Substring(3, 1)) * 7 +
                int.Parse(iptID.Substring(4, 1)) * 6 +
                int.Parse(iptID.Substring(5, 1)) * 5 +
                int.Parse(iptID.Substring(6, 1)) * 4 +
                int.Parse(iptID.Substring(7, 1)) * 3 +
                int.Parse(iptID.Substring(8, 1)) * 2 +
                int.Parse(iptID.Substring(9, 1)) +
                int.Parse(iptID.Substring(10, 1));
            //判斷是否可整除   
            if ((s % 10) != 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion

        /// <summary>
        /// 檢查上傳檔案是否為圖片格式
        /// </summary>
        /// <param name="fileName">完整檔案名稱</param>
        /// <returns>True or False</returns>
        public static Boolean CheckPhotoFormat(String fileName)
        {
            Boolean flag = false;
            String fileExtension = Path.GetExtension(fileName).ToLower();
            String[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (allowedExtensions[i].ToString().Equals(fileExtension))
                {
                    flag = true;
                }
            }

            return flag;
        }

    }
}
