using System;
using System.Collections.Generic;
using System.Linq;
using DBHelper.SQLHelper;
using Hoshino.Email.Entity;

namespace Hoshino.Email.Repository
{
    public class EmailAccountRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        public bool Insert(EmailAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailAccountID != null)
            {
                dic["EmailAccountID"] = model.EmailAccountID;
            }
            if (model.EmailAccountAddress != null)
            {
                dic["EmailAccountAddress"] = model.EmailAccountAddress;
            }
            if (model.EmailAccountPassWord != null)
            {
                dic["EmailAccountPassWord"] = model.EmailAccountPassWord;
            }
            if (model.EmailAccountName != null)
            {
                dic["EmailAccountName"] = model.EmailAccountName;
            }
            if (model.EmailAccountSMTP != null)
            {
                dic["EmailAccountSMTP"] = model.EmailAccountSMTP;
            }
            if (model.EmailAccountSMTPPort != null && model.EmailAccountSMTPPort.HasValue)
            {
                dic["EmailAccountSMTPPort"] = model.EmailAccountSMTPPort;
            }
            if (model.EmailAccountPOP3 != null)
            {
                dic["EmailAccountPOP3"] = model.EmailAccountPOP3;
            }
            dic["EmailAccountPOP3Port"] = model.EmailAccountPOP3Port;
            dic["EmailAccountIsSSL"] = model.EmailAccountIsSSL;
            dic["EmailAccountMaxEmailCount"] = model.EmailAccountMaxEmailCount;
            dic["EmailAccountSpace"] = model.EmailAccountSpace;
            dic["SendState"] = model.SendState;
            dic["SendMode"] = model.SendMode;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailaccount", dic) > 0;
        }

        /// <summary>
        /// 修改剩余数量和下次发送时间
        /// </summary>
        public bool UpdateRemainCount(string EmailAccountID, int EmailAccountRemainEmailCount, DateTime EmailAccountNextSendTime)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountID"] = EmailAccountID;
            dic["EmailAccountRemainEmailCount"] = EmailAccountRemainEmailCount;
            dic["EmailAccountNextSendTime"] = EmailAccountNextSendTime;
            return SQLHelperFactory.Instance.ExecuteNonQuery("UpdateEmailAccount_RemainCount", dic) > 0;
        }

        /// <summary>
        /// 修改占用
        /// </summary>
        public bool UpdateEmailAccountOccupy(EmailAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountID"] = model.EmailAccountID;
            dic["OccupyIP"] = model.OccupyIP;
            return SQLHelperFactory.Instance.ExecuteNonQuery("UpdateEmailAccount_Occupy", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(string EmailAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountID"] = EmailAccountID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailaccount", dic) > 0;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailAccountEntity Get(string EmailAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountID"] = EmailAccountID;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailAccountEntity>("Select_EmailAccount", dic);
        }

        private List<EmailAccountEntity> EmailAccountList
        {
            get
            {

                List<EmailAccountEntity> list = new List<EmailAccountEntity>();
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "a13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "b13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "c13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "d13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "e13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "f13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "g13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "h13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "i13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "j13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "k13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "l13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "m13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "n13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "o13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "p13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "q13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "r13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类1", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "s13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 0 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "t13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "u13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "v13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "w13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "x13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "y13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                list.Add(new EmailAccountEntity() { EmailAccountAddress = "z13456789@qq.com", EmailAccountCreateTime = DateTime.Now, Group = "分类2", SendState = 1 });
                return list;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public (IEnumerable<EmailAccountEntity>, int) GetList(string address, string group, int pageindex, int pagesize)
        {
            //List<EmailAccountEntity> list = this.EmailAccountList.Where(l => (string.IsNullOrWhiteSpace(group) || l.Group.IndexOf(group) >= 0) && (string.IsNullOrWhiteSpace(address) || l.EmailAccountAddress.IndexOf(address) >= 0)).OrderBy(l => l.EmailAccountAddress).Skip(pageindex <= 1 ? 0 : (pageindex - 1) * pagesize).Take(pagesize).ToList();
            //return (list, this.EmailAccountList.Count);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(address))
            {
                dic["EmailAccountAddress"] = address;
            }
            if (!string.IsNullOrWhiteSpace(group))
            {
                dic["Group"] = group;
            }
            if (pageindex >= 0)
            {
                dic["StartIndex"] = pageindex <= 1 ? 0 : (pageindex - 1) * pagesize;
            }
            if (pagesize > 0)
            {
                dic["SelectCount"] = pagesize;
            }
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<EmailAccountEntity>("Select_EmailAccount_List_By_Page", dic, out int total);
            return (list, total);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public IEnumerable<EmailAccountEntity> GetList()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var list = SQLHelperFactory.Instance.QueryForListByT<EmailAccountEntity>("Select_EmailAccount_List", dic);
            return list;
        }

    }
}
