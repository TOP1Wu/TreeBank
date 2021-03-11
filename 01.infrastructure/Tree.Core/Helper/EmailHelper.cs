using System.Net;
using System.Net.Mail;
using System.Text;

namespace Tree.Core.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// 功能：异步发送邮件。Task.Factory.StartNew(() =>{ SendMails()});
        /// 
        /// TODO: 将来要假如 redis 。。。
        /// 
        /// </summary>
        /// <param name="mailTo">MailTo为收信人地址</param>
        /// <param name="subject">Subject为标题</param>
        /// <param name="body">Body为信件内容</param>
        /// <param name="useTemplate"></param>
        public static bool
            SendMails(string[] mailTo, string subject, string body, bool useTemplate = true) //string mailTo,
        {
            try
            {
                /*******************************************************************************
                *        Code Review
                *
                * CreateTime: 2018/8/18 00:08
                * Author：  linkanyway
                * Description： 
                *
                * review details
                * ───────────────────────────────────
                * Original
                *
                *
                *
                *
                *  Changed
                *      整合new
                *
                *********************************************************************************/

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("System@Lyrewing.com", "System"), //发件人
                    Subject = subject, //主题
                    SubjectEncoding = Encoding.UTF8, //邮件主题编码
                    Body = body, //内容
                    BodyEncoding = Encoding.UTF8, //正文编码
                    IsBodyHtml = true, //设置为HTML格式
                    Priority = MailPriority.High //优先级
                };

                foreach (var s in mailTo)
                {
                    mailMessage.To.Add(new MailAddress(s));
                }

                var smtpClient = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network, //指定电子邮件发送方式
                    Host = "smtp.mxhichina.com", //指定SMTP服务器
                    Port = 25,
                    Credentials = new NetworkCredential("System@Lyrewing.com", "ruff1981$"), //用户名和密码
                    EnableSsl = false
                };

                //异步发送
                smtpClient.SendMailAsync(mailMessage);
            }
            catch (System.Exception)
            {
                ErrorCallback();
                return false;
            }

            return true;
        }


        private static void ErrorCallback()
        {
            //throw new NotImplementedException();
        }
    }
}
