using System;
using System.Collections.Generic;
using DBHelper.SQLHelper;
using Hoshino.Email.Entity;

namespace Hoshino.Email.Repository
{
    public class EmailSendBccAccountRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        public bool Insert(EmailSendBccAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic["EmailID"] = model.EmailID;
            dic["EmailBccAccountID"] = model.EmailBccAccountID;
            dic["EmailSendBccAccountState"] = model.EmailSendBccAccountState;

            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailsendbccaccount", dic) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(int EmailID,int newState, int oldState)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailID"] = EmailID;

            //if (model.EmailSendBccAccountID != null)
            //{
            //    dic["EmailSendBccAccountID"] = model.EmailSendBccAccountID;
            //}

            //if (model.EmailBccAccountID != null)
            //{
            //    dic["EmailBccAccountID"] = model.EmailBccAccountID;
            //}
            //if (model.EmailAccountID != null)
            //{
            //    dic["EmailAccountID"] = model.EmailAccountID;
            //}
            dic["EmailSendBccAccountState"] = newState;
            dic["OldState"] = oldState;
            //dic["EmailSendBccAccountCreateTime"] = model.EmailSendBccAccountCreateTime;
            //dic["EmailSendBccAccountLastTime"] = model.EmailSendBccAccountLastTime;
            //dic["EmailSendBccAccountSendTime"] = model.EmailSendBccAccountSendTime;
            //dic["Result"] = model.Result;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailsendbccaccount", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(int EmailSendBccAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailSendBccAccountID"] = EmailSendBccAccountID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailsendbccaccount", dic) > 0;
        }

        /// <summary>
        /// 根據emailID刪除數據
        /// </summary>
        public bool DeleteAllByEmailID(int EmailID,int EmailSendBccAccountState=-2)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailID"] = EmailID;
            if (EmailSendBccAccountState > -2)
            {
                dic["EmailSendBccAccountState"] = EmailSendBccAccountState;
            }
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailsendbccaccount_all", dic) > 0;
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailSendBccAccountEntity Get(string EmailSendBccAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailSendBccAccountID"] = EmailSendBccAccountID;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailSendBccAccountEntity>("Select_emailsendbccaccount", dic);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public (IEnumerable<EmailSendBccAccountEntity>, int) GetList(int EmailID, string EmailBccAccountAddress, int pageindex, int pagesize)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailID"] = EmailID;
            if (!string.IsNullOrWhiteSpace(EmailBccAccountAddress))
            {
                dic["EmailBccAccountAddress"] = EmailBccAccountAddress;
            }
            if (pageindex >= 0)
            {
                dic["StartIndex"] = pageindex <= 1 ? 0 : (pageindex - 1) * pagesize;
            }
            if (pagesize > 0)
            {
                dic["SelectCount"] = pagesize;
            }
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<EmailSendBccAccountEntity>("Select_emailsendbccaccount_List", dic, out int total);
            return (list, total);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public IEnumerable<EmailSendBccAccountEntity> GetListByEmailAccountAndEmailID(int EmailID, int EmailAccountID, int SendCount)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailID"] = EmailID;
            dic["EmailAccountID"] = EmailAccountID;
            dic["SendCount"] = SendCount;
            var list = SQLHelperFactory.Instance.QueryForListByT<EmailSendBccAccountEntity>("Select_EmailSendBccAccount_List_ByEmailAccountAndEmailID", dic);
            return list;
        }


        /// <summary>
        /// 修改
        /// </summary>
        public bool UpdateState(List<EmailSendBccAccountEntity> list)
        {
            List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
            foreach (var model in list)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (model.EmailSendBccAccountID != null)
                {
                    dic["EmailSendBccAccountID"] = model.EmailSendBccAccountID;
                }
                dic["EmailSendBccAccountState"] = model.EmailSendBccAccountState;
                dic["Result"] = "无";
                if (!string.IsNullOrWhiteSpace(model.Result))
                {
                    dic["Result"] = model.Result;
                }
                dicList.Add(dic);
            }
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailsendbccaccount_state", dicList) > 0;
        }

        /// <summary>
        /// 获取根据状态需要导出的列表列表
        /// </summary>
        public IEnumerable<EmailBccAccountEntity> GetExportEmailBccAccountList(int EmailID, int state)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailID"] = EmailID;
            dic["EmailSendBccAccountState"] = state;
            var list = SQLHelperFactory.Instance.QueryForListByT<EmailBccAccountEntity>("Select_ExportEmailBccAccount_List", dic);
            return list;
        }
    }
}
