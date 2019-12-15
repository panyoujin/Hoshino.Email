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
            dic["EmailAccountSMTPPort"] = model.EmailAccountSMTPPort;
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
            dic["EmailAccountCategoryID"] = model.EmailAccountCategoryID;
            dic["EmailAccountCategoryName"] = model.EmailAccountCategoryName;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailaccount", dic) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(EmailAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountID"] = model.EmailAccountID;
            dic["EmailAccountAddress"] = model.EmailAccountAddress;
            dic["EmailAccountPassWord"] = model.EmailAccountPassWord;
            dic["EmailAccountName"] = model.EmailAccountName;
            dic["EmailAccountSMTP"] = model.EmailAccountSMTP;
            dic["EmailAccountSMTPPort"] = model.EmailAccountSMTPPort;
            dic["EmailAccountPOP3"] = model.EmailAccountPOP3;
            dic["EmailAccountPOP3Port"] = model.EmailAccountPOP3Port;
            dic["EmailAccountIsSSL"] = model.EmailAccountIsSSL;
            dic["EmailAccountMaxEmailCount"] = model.EmailAccountMaxEmailCount;
            dic["EmailAccountSpace"] = model.EmailAccountSpace;
            dic["SendState"] = model.SendState;
            dic["SendMode"] = model.SendMode;
            dic["EmailAccountCategoryID"] = model.EmailAccountCategoryID;
            dic["EmailAccountCategoryName"] = model.EmailAccountCategoryName;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailaccount", dic) > 0;
        }

        /// <summary>
        /// 修改剩余数量和下次发送时间
        /// </summary>
        public bool UpdateRemainCount(int EmailAccountID, int EmailAccountRemainEmailCount, DateTime EmailAccountNextSendTime)
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
        public bool Delete(int EmailAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountID"] = EmailAccountID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailaccount", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(string EmailAccountAddress)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountAddress"] = EmailAccountAddress;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailaccount", dic) > 0;
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailAccountEntity Get(int EmailAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountID"] = EmailAccountID;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailAccountEntity>("Select_EmailAccount", dic);
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailAccountEntity GetByAddress(string EmailAccountAddress)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountAddress"] = EmailAccountAddress;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailAccountEntity>("Select_EmailAccountByAddress", dic);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public (IEnumerable<EmailAccountEntity>, int) GetList(string address, string group, int pageindex, int pagesize)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(address))
            {
                dic["EmailAccountAddress"] = address;
            }
            if (!string.IsNullOrWhiteSpace(group))
            {
                dic["EmailAccountCategoryName"] = group;
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
        public IEnumerable<EmailAccountEntity> GetEmailAccountList(string address, string group)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(address))
            {
                dic["EmailAccountAddress"] = address;
            }
            if (!string.IsNullOrWhiteSpace(group))
            {
                dic["EmailAccountCategoryName"] = group;
            }
            var list = SQLHelperFactory.Instance.QueryForListByT<EmailAccountEntity>("Select_EmailAccount_All", dic);
            return list;
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
