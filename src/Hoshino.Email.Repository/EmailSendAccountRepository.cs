using System;
using System.Collections.Generic;
using DBHelper.SQLHelper;
using Hoshino.Email.Entity;

namespace Hoshino.Email.Repository
{
    public class EmailSendAccountRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        public bool Insert(EmailSendAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailSendAccountID != null)
            {
                dic["EmailSendAccountID"] = model.EmailSendAccountID;
            }
            if (model.EmailID != null)
            {
                dic["EmailID"] = model.EmailID;
            }
            if (model.EmailAccountID != null)
            {
                dic["EmailAccountID"] = model.EmailAccountID;
            }
            dic["EmailSendAccountCreateTime"] = model.EmailSendAccountCreateTime;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailsendaccount", dic) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(EmailSendAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailSendAccountID != null)
            {
                dic["EmailSendAccountID"] = model.EmailSendAccountID;
            }
            if (model.EmailID != null)
            {
                dic["EmailID"] = model.EmailID;
            }
            if (model.EmailAccountID != null)
            {
                dic["EmailAccountID"] = model.EmailAccountID;
            }
            dic["EmailSendAccountCreateTime"] = model.EmailSendAccountCreateTime;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailsendaccount", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(string EmailSendAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailSendAccountID"] = EmailSendAccountID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailsendaccount", dic) > 0;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public T Get<T>(string EmailSendAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailSendAccountID"] = EmailSendAccountID;
            return SQLHelperFactory.Instance.QueryForObjectByT<T>("Select_emailsendaccount", dic);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public (IEnumerable<T>, int) GetList<T>(int pageindex, int pagesize)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (pageindex >= 0)
            {
                dic["StartIndex"] = pageindex <= 1 ? 0 : (pageindex - 1) * pagesize;
            }
            if (pagesize > 0)
            {
                dic["SelectCount"] = pagesize;
            }
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<T>("Select_emailsendaccount_List", dic, out int total);
            return (list, total);
        }

    }
}
