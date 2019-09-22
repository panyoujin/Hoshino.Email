using System;
using System.Collections.Generic;
using DBHelper.SQLHelper;
using Hoshino.Email.Entity;

namespace Hoshino.Email.Repository
{
    public class EmailBccAccountRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        public bool Insert(EmailBccAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailBccAccountID != null)
            {
                dic["EmailBccAccountID"] = model.EmailBccAccountID;
            }
            if (model.EmailBccAccountAddress != null)
            {
                dic["EmailBccAccountAddress"] = model.EmailBccAccountAddress;
            }
            if (model.EmailBccAccountName != null)
            {
                dic["EmailBccAccountName"] = model.EmailBccAccountName;
            }
            dic["EmailBccAccountCreateTime"] = model.EmailBccAccountCreateTime;
            dic["EmailBccAccountLastTime"] = model.EmailBccAccountLastTime;
            dic["EmailBccAccountIsDel"] = model.EmailBccAccountIsDel;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailbccaccount", dic) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(EmailBccAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailBccAccountID != null)
            {
                dic["EmailBccAccountID"] = model.EmailBccAccountID;
            }
            if (model.EmailBccAccountAddress != null)
            {
                dic["EmailBccAccountAddress"] = model.EmailBccAccountAddress;
            }
            if (model.EmailBccAccountName != null)
            {
                dic["EmailBccAccountName"] = model.EmailBccAccountName;
            }
            dic["EmailBccAccountCreateTime"] = model.EmailBccAccountCreateTime;
            dic["EmailBccAccountLastTime"] = model.EmailBccAccountLastTime;
            dic["EmailBccAccountIsDel"] = model.EmailBccAccountIsDel;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailbccaccount", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(string EmailBccAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailBccAccountID"] = EmailBccAccountID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailbccaccount", dic) > 0;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public T Get<T>(string EmailBccAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailBccAccountID"] = EmailBccAccountID;
            return SQLHelperFactory.Instance.QueryForObjectByT<T>("Select_emailbccaccount", dic);
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
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<T>("Select_emailbccaccount_List", dic, out int total);
            return (list, total);
        }

    }
}
