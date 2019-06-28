using LumiSoft.Net.AUTH;
using LumiSoft.Net.Mail;
using LumiSoft.Net.Mime;
using LumiSoft.Net.MIME;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Hoshino.Email.Core.Helper
{
    /// <summary>
    /// 郵件幫助類
    /// </summary>
    /// river.xu ADD 2014/07/20
    public class MailHelper
    {
        /// <summary>
        /// 發送eml文件
        /// </summary>
        /// <param name="authhh"></param>
        /// <param name="toMail"></param>
        /// <param name="pathMel"></param>
        public static void SendMailToMel(AUTH_SASL_Client_Plain authhh, string toMail, string pathMel, string smtp, int port, bool ssl)
        {
            try
            {
                using (LumiSoft.Net.SMTP.Client.SMTP_Client client = new LumiSoft.Net.SMTP.Client.SMTP_Client())
                {
                    client.Connect(smtp, port, ssl);
                    client.EhloHelo(smtp);
                    client.Auth(authhh);
                    client.RcptTo(toMail);
                    var msg = Mail_Message.ParseFromFile(pathMel);

                    MemoryStream stream = new MemoryStream();
                    msg.ToStream(stream, new MIME_Encoding_EncodedWord(MIME_EncodedWordEncoding.Q, Encoding.UTF8), Encoding.UTF8);
                    stream.Position = 0;
                    client.SendMessage(stream);
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建Mime类型格式邮件
        /// </summary>
        /// <param name="tomails"></param>
        /// <param name="ccmails"></param>
        /// <param name="mailFrom"></param>
        /// <param name="mailFromDisplay"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        /// <param name="embedImages"></param>
        /// <param name="notifyEmail"></param>
        /// <param name="plaintTextTips"></param>
        /// <param name="replyEmail"></param>
        /// <returns></returns>
        public static Mime Create_HtmlBody_Attachment_Image(Dictionary<string, string> tomails, Dictionary<string, string> ccmails,
            string mailFrom, string mailFromDisplay, string subject, string body, List<string> attachments,
            Dictionary<string, string> embedImages, string notifyEmail = "", string plaintTextTips = "", string replyEmail = "")
        {
            Mime m = new Mime();
            MimeEntity mainEntity = m.MainEntity;

            #region 设置收件箱
            if (tomails != null)
            {
                mainEntity.To = new AddressList();
                foreach (string address in tomails.Keys)
                {
                    string displayName = tomails[address];
                    mainEntity.To.Add(new MailboxAddress(displayName, address));
                }
            }
            #endregion

            #region 设置抄送箱
            if (ccmails != null)
            {
                mainEntity.Cc = new AddressList();
                foreach (string address in ccmails.Keys)
                {
                    string displayName = ccmails[address];
                    mainEntity.Cc.Add(new MailboxAddress(displayName, address));
                }
            }
            #endregion

            #region 设置发件箱
            if (!string.IsNullOrWhiteSpace(mailFrom))
            {
                mainEntity.From = new AddressList();
                mainEntity.From.Add(new MailboxAddress(mailFromDisplay, mailFrom));
            }
            #endregion

            mainEntity.Subject = subject;
            mainEntity.ContentType = MediaType_enum.Multipart_mixed;

            #region 设置回执通知
            if (!string.IsNullOrEmpty(notifyEmail) && IsEmail(notifyEmail))
            {
                mainEntity.DSN = notifyEmail;
            }
            #endregion

            MimeEntity textEntity = mainEntity.ChildEntities.Add();
            textEntity.ContentType = MediaType_enum.Text_html;
            textEntity.ContentTransferEncoding = ContentTransferEncoding_enum.QuotedPrintable;
            textEntity.DataText = body;

            //附件
            foreach (string attach in attachments)
            {
                MimeEntity attachmentEntity = mainEntity.ChildEntities.Add();
                attachmentEntity.ContentType = MediaType_enum.Application_octet_stream;
                attachmentEntity.ContentDisposition = ContentDisposition_enum.Attachment;
                attachmentEntity.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
                FileInfo file = new FileInfo(attach);
                attachmentEntity.ContentDisposition_FileName = file.Name;
                attachmentEntity.DataFromFile(attach);
            }

            //嵌入图片
            foreach (string key in embedImages.Keys)
            {
                MimeEntity attachmentEntity = mainEntity.ChildEntities.Add();
                attachmentEntity.ContentType = MediaType_enum.Application_octet_stream;
                attachmentEntity.ContentDisposition = ContentDisposition_enum.Inline;
                attachmentEntity.ContentTransferEncoding = ContentTransferEncoding_enum.Base64;
                string imageFile = embedImages[key];
                FileInfo file = new FileInfo(imageFile);
                attachmentEntity.ContentDisposition_FileName = file.Name;

                //string displayName = Path.GetFileNameWithoutExtension(fileName);
                attachmentEntity.ContentID = key;//BytesTools.BytesToHex(Encoding.Default.GetBytes(fileName));

                attachmentEntity.DataFromFile(imageFile);
            }
            return m;
        }


        public static Mail_Message Create_PlainText_Html_Attachment_Image(Dictionary<string, string> tomails, Dictionary<string, string> ccmails, string mailFrom, string mailFromDisplay,
    string subject, string body, Dictionary<string, string> attachments, string notifyEmail = "", string plaintTextTips = "")
        {
            Mail_Message msg = new Mail_Message();
            msg.MimeVersion = "1.0";
            msg.MessageID = MIME_Utils.CreateMessageID();
            msg.Date = DateTime.Now;
            msg.Subject = subject;
            msg.From = new Mail_t_MailboxList();
            msg.From.Add(new Mail_t_Mailbox(mailFromDisplay, mailFrom));
            msg.To = new Mail_t_AddressList();
            foreach (string address in tomails.Keys)
            {
                string displayName = tomails[address];
                msg.To.Add(new Mail_t_Mailbox(displayName, address));
            }
            msg.Cc = new Mail_t_AddressList();
            foreach (string address in ccmails.Keys)
            {
                string displayName = ccmails[address];
                msg.Cc.Add(new Mail_t_Mailbox(displayName, address));
            }

            //设置回执通知
            if (!string.IsNullOrEmpty(notifyEmail) && IsEmail(notifyEmail))
            {
                msg.DispositionNotificationTo.Add(new Mail_t_Mailbox(notifyEmail, notifyEmail));
            }

            #region MyRegion

            //--- multipart/mixed -----------------------------------
            MIME_h_ContentType contentType_multipartMixed = new MIME_h_ContentType(MIME_MediaTypes.Multipart.mixed);
            contentType_multipartMixed.Param_Boundary = Guid.NewGuid().ToString().Replace('-', '.');
            MIME_b_MultipartMixed multipartMixed = new MIME_b_MultipartMixed(contentType_multipartMixed);
            msg.Body = multipartMixed;

            //--- multipart/alternative -----------------------------
            MIME_Entity entity_multipartAlternative = new MIME_Entity();
            MIME_h_ContentType contentType_multipartAlternative = new MIME_h_ContentType(MIME_MediaTypes.Multipart.alternative);
            contentType_multipartAlternative.Param_Boundary = Guid.NewGuid().ToString().Replace('-', '.');
            MIME_b_MultipartAlternative multipartAlternative = new MIME_b_MultipartAlternative(contentType_multipartAlternative);
            entity_multipartAlternative.Body = multipartAlternative;
            multipartMixed.BodyParts.Add(entity_multipartAlternative);

            //--- text/plain ----------------------------------------
            MIME_Entity entity_text_plain = new MIME_Entity();
            MIME_b_Text text_plain = new MIME_b_Text(MIME_MediaTypes.Text.plain);
            entity_text_plain.Body = text_plain;

            //普通文本邮件内容，如果对方的收件客户端不支持HTML，这是必需的
            string plainTextBody = "如果你邮件客户端不支持HTML格式，或者你切换到“普通文本”视图，将看到此内容";
            if (!string.IsNullOrEmpty(plaintTextTips))
            {
                plainTextBody = plaintTextTips;
            }

            text_plain.SetText(MIME_TransferEncodings.QuotedPrintable, Encoding.UTF8, plainTextBody);
            multipartAlternative.BodyParts.Add(entity_text_plain);

            //--- text/html -----------------------------------------
            string htmlText = body;//"<html>这是一份测试邮件，<img src=\"cid:test.jpg\">来自<font color=red><b>LumiSoft.Net</b></font></html>";
            MIME_Entity entity_text_html = new MIME_Entity();
            MIME_b_Text text_html = new MIME_b_Text(MIME_MediaTypes.Text.html);
            entity_text_html.Body = text_html;
            text_html.SetText(MIME_TransferEncodings.QuotedPrintable, Encoding.UTF8, htmlText);
            multipartAlternative.BodyParts.Add(entity_text_html);

            //--- application/octet-stream -------------------------
            WebClient client = new WebClient();
            foreach (string attach in attachments.Keys)
            {
                try
                {
                    byte[] bytes = client.DownloadData(attachments[attach]);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        multipartMixed.BodyParts.Add(Mail_Message.CreateAttachment(stream, attach));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            return msg;
        }


        /// <summary>
        /// 查詢htmlBody的郵箱數量
        /// </summary>
        /// <param name="sHtmlText"></param>
        /// <returns></returns>
        public static List<string> GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签   
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(sHtmlText);
            List<string> sUrlList = new List<string>();

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList.Add(match.Groups["imgUrl"].Value);
            return sUrlList;
        }


        /// <summary>
        /// 判断邮箱地址是否正确
        /// </summary>
        /// <param name="mailAddress">邮箱地址参数</param>
        /// <returns></returns>
        public static bool IsEmail(string mailAddress)
        {
            Regex r = new Regex("^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$");
            if (r.Match(mailAddress).Success)
            {
                return true;
            }
            return false;
        }

        public static Dictionary<string, string> GetPathByImage(MIME_Entity[] entitys)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string path = Directory.GetCurrentDirectory() + "\\SendingPic";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var one in entitys)
            {
                if (one.ContentType.SubType.ToLower() == "jpeg" || one.ContentType.SubType.ToLower() == "gif" || one.ContentType.SubType.ToLower() == "png")
                {
                    try
                    {
                        string savePath = string.Format("{0}\\{1}.jpg", path, Guid.NewGuid());
                        MIME_b_SinglepartBase byteObj = (MIME_b_SinglepartBase)one.Body;
                        MemoryStream stream = new MemoryStream(byteObj.Data);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                        img.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        string key = one.ContentID.Replace("<", "cid:");
                        key = key.Replace(">", string.Empty);
                        dic.Add(key, savePath);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return dic;
        }

        public static List<Stream> GetEmlImageStream(MIME_Entity[] entitys)
        {
            List<Stream> streamList = new List<Stream>();
            foreach (var one in entitys)
            {
                if (one.ContentType.SubType == "jpeg")
                {
                    try
                    {
                        MIME_b_SinglepartBase byteObj = (MIME_b_SinglepartBase)one.Body;
                        MemoryStream stream = new MemoryStream(byteObj.Data);
                        streamList.Add(stream);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return streamList;
        }
    }
}
