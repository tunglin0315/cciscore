using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Project.Common.Helper
{

    /// <summary>
    /// 郵件類別庫
    /// </summary>
    public class MailHelper
    {

        /// <summary>
        /// 信件發送. send
        /// </summary>
        /// <param name="msg">信件內容.</param>
        /// <returns></returns>
        public static bool send(ref MailMessage msg)
        {
            SmtpClient client = new SmtpClient("msa.eri.com.tw");
            client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential(< user >, < pwd >);
            //client.Credentials = new NetworkCredential("peter", "");

            //沒設 From 加入系統預設 From
            if (msg.From == null) msg.From = new MailAddress("mailfrom@eri.com.tw", ".NET Core MAIL");
            msg.BodyEncoding = Encoding.UTF8;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            client.Send(msg);

            return true;
        }
    }
}
