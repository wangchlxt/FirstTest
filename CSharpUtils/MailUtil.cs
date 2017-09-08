using CSharpUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
   public class MailUtil
    {
        #region

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mailTo">要发送的邮箱</param>
        /// <param name="mailSubject">邮箱主题</param>
        /// <param name="mailContent">邮箱内容</param>
        /// <returns>返回发送邮箱的结果</returns>
        public static bool SendEmail(string mailTo, string mailSubject, string mailContent, string imgPath = null)
        {
            // 设置发送方的邮件信息,例如使用网易的smtp
            string smtpServer = ConfigurationManager.AppSettings["SmtpServer"].ToString().Trim();//SMTP服务器
            string mailFrom = ConfigurationManager.AppSettings["MailFrom"].ToString().Trim(); //登陆用户名
            string userPassword = ConfigurationManager.AppSettings["MailPassword"].ToString().Trim();//登陆密码

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();

            //smtpClient.EnableSsl = true;
            //smtpClient.UseDefaultCredentials = false;

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置
            MailMessage mailMessage = new MailMessage(); // 发送人和收件人
            mailMessage.From= new MailAddress(mailFrom);
            var tos= mailTo.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            tos.ForEach(to => { mailMessage.To.Add(to); });
            
            mailMessage.Subject = mailSubject;//主题
            mailMessage.Body = mailContent;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Low;//优先级
            try
            {
                if (imgPath != null)
                {
                    mailMessage.Attachments.Add(new Attachment(imgPath));
                    mailMessage.Attachments[0].ContentType.Name = "image/jpg";
                    mailMessage.Attachments[0].ContentId = "img0";
                    mailMessage.Attachments[0].ContentDisposition.Inline = true;
                    mailMessage.Attachments[0].TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                }
                smtpClient.Send(mailMessage); // 发送邮件
                return true;
            }
            catch (SmtpException ex)
            {
                LogHelper.Error(ex.ToString());
                return false;
            }
        }

        #endregion
    }
}
