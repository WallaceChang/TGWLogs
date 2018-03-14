using System;
using System.Text;
using System.Net.Mail;

namespace TGWLogs.App_Code
{
    public class clsMail
    {
        #region 編碼類型
        public enum EncodingType
        {
            BIG5 = 0,
            UTF8 = 1,
            Unicode = 2
        }
        #endregion

        #region 郵件格式
        public enum MailFormat
        {
            Html = 1,
            Normal = 0,
        }
        #endregion

        public clsMail()
        {
            //建構式
        }

        #region mail寄送
        public void SendMail(string strHostIP, int intPort, string strMailSender, string strMailReciever, string strSubject, string strMailBody,
                             string strCC, string strBCC, string[] strAttachPath, MailFormat intFormat = MailFormat.Html,
                             MailPriority intPriority = MailPriority.High, EncodingType intEncode = EncodingType.UTF8)
        {
            SmtpClient objSmtp = null;
            MailMessage objMMS = null;
            Attachment objAttach = null;
            int intI = 0;
            try
            {
                /*指定Mail Server & Port*/
                objSmtp = new SmtpClient(strHostIP, intPort);

                /*設定郵件主體*/
                objMMS = new MailMessage();
                objMMS.From = new MailAddress(strMailSender);
                objMMS.Subject = strSubject;
                objMMS.Body = strMailBody;

                /*設定收件人*/
                objMMS.To.Add(strMailReciever);

                /*設定副本收件人*/
                if (strCC.Length > 0)
                {
                    objMMS.CC.Add(strCC);
                }

                /*設定密件副本收件人*/
                if (strBCC.Length > 0)
                {
                    objMMS.Bcc.Add(strBCC);
                }

                /*設定郵件重要性*/
                objMMS.Priority = intPriority;

                /*設定郵件格式*/
                objMMS.IsBodyHtml = Convert.ToBoolean(intFormat);

                /*設定郵件編碼*/
                switch (intEncode)
                {
                    case EncodingType.UTF8:
                        objMMS.BodyEncoding = Encoding.UTF8;
                        break;
                    case EncodingType.Unicode:
                        objMMS.BodyEncoding = Encoding.Unicode;
                        break;
                    default:
                        objMMS.BodyEncoding = Encoding.Default;
                        break;
                }

                /*設定附件*/
                for (intI = 0; intI < strAttachPath.Length; intI++)
                {
                    objAttach = new Attachment(strAttachPath[intI]);
                    objMMS.Attachments.Add(objAttach);
                }

                /*信件發送*/
                objSmtp.Send(objMMS);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objMMS.Dispose();
                objSmtp.Dispose();
            }
        }
        #endregion
    }
}