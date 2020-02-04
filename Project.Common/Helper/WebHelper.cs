using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Common.Helper
{
    public class WebHelper
    {

        #region <自動產生編號>
        /// <summary>
        ///  自動產生編號
        /// </summary>
        /// <returns>TYYYYMMDD</returns>
        public static string Repairid()
        {
            DateTime objDate = DateTime.Now;
            int objYearTemp = objDate.Year - 1911;
            String objYear = objYearTemp.ToString();
            String objMonth = objDate.Month.ToString();
            String objDay = objDate.Day.ToString();


            if (objYear.Length < 3)
            {
                objYear = "0" + objYear;
            }
            if (objMonth.Length < 2)
            {
                objMonth = "0" + objMonth;
            }
            if (objDay.Length < 2)
            {
                objDay = "0" + objDay;
            }
            return "T" + objYear + objMonth + objDay;
        }
        #endregion

        #region <IP>

        /// <summary>
        /// 檢查 IP 是否合法
        /// </summary>
        /// <param name="strPattern">需檢測的 IP</param>
        /// <returns>true:合法 false:不合法</returns>
        public static bool CheckIP(string strPattern)
        {
            // 繼承自：System.Text.RegularExpressions
            // regular: ^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^\\d{1,3}[\\.]\\d{1,3}[\\.]\\d{1,3}[\\.]\\d{1,3}$");
            System.Text.RegularExpressions.Match m = regex.Match(strPattern);

            return m.Success;
        }
        #endregion
    }
}
