using CDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{
    class Program
    {
        private static string ToBase64(string instr)
        {
            byte[] bt = Encoding.GetEncoding("BIG5").GetBytes(instr);
            return Convert.ToBase64String(bt);
        }

        static void Main(string[] args)
        {
            try
            {
                #region 设置基本信息
                CDO.Message oMsg = new CDO.Message();

                Configuration conf = new ConfigurationClass();
                conf.Fields[CdoConfiguration.cdoSendUsingMethod].Value = CdoSendUsing.cdoSendUsingPort;
                conf.Fields[CdoConfiguration.cdoSMTPAuthenticate].Value = CdoProtocolsAuthentication.cdoBasic;
                conf.Fields[CdoConfiguration.cdoSMTPUseSSL].Value = false;
                conf.Fields[CdoConfiguration.cdoSMTPServer].Value = "smtp.sina.com";//必填，而且要真实可用   
                conf.Fields[CdoConfiguration.cdoSMTPServerPort].Value = 25;
                conf.Fields[CdoConfiguration.cdoSendEmailAddress].Value = "xuxujiang00@sina.com";
                conf.Fields[CdoConfiguration.cdoSendUserName].Value = "xuxujiang00@sina.com";//真实的邮件地址   
                conf.Fields[CdoConfiguration.cdoSendPassword].Value = "5e5d277c167bd275";   //为邮箱密码，必须真实   
                //5e5d277c167bd275
                conf.Fields.Update();
                oMsg.Configuration = conf;
                #endregion 设置基本信息

                #region htmlbody

                string bodyStr = "test11";
                //List<string> strList = MailHelper.GetHtmlImageUrlList(bodyStr);
                //Dictionary<string, string> dicImage = new Dictionary<string, string>();
                //foreach (var str in strList)
                //{
                //    string key = Guid.NewGuid().ToString();
                //    string newUrl = "cid:" + key;
                //    bodyStr = bodyStr.Replace(str, newUrl);
                //    dicImage.Add(key, str);
                //}
                oMsg.HTMLBody = bodyStr;
                #endregion
                StringBuilder title = new StringBuilder();
                title.Append("=?BIG5?B?");
                title.Append(ToBase64("titleTest"));
                title.Append("?=");
                oMsg.Subject = title.ToString();
                oMsg.From = "\"" + "大獎" + "\"" + "xuxujiang00@sina.com";
                ;//真实的邮件地址   
                #region BCC
                StringBuilder bccs = new StringBuilder();
                bccs.Append("1842125096@qq.com;");


                oMsg.BCC = bccs.ToString();
                #endregion BCC
                oMsg.HTMLBodyPart.Charset = "BIG5";


                oMsg.Send();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
