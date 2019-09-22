using System;
using System.Collections.Generic;
using DBHelper.SQLHelper;
using Hoshino.Email.Entity;

namespace Hoshino.Email.Repository
{
    public class EmailSendFailureRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        public bool Insert(List<EmailSendFailureEntity> list)
        {
            List<Dictionary<string, object>> dicList = new List<Dictionary<string, object>>();
            foreach (var model in list)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (model.EmailSendFailureID != null)
                {
                    dic["EmailSendFailureID"] = model.EmailSendFailureID;
                }
                if (model.EmailID != null)
                {
                    dic["EmailID"] = model.EmailID;
                }
                if (model.EmailBccAccountID != null)
                {
                    dic["EmailBccAccountID"] = model.EmailBccAccountID;
                }
                if (model.EmailAccountID != null)
                {
                    dic["EmailAccountID"] = model.EmailAccountID;
                }
                dic["EmailSendFailureSendTime"] = model.EmailSendFailureSendTime;
                dic["Result"] = "无";
                if (string.IsNullOrWhiteSpace(model.Result))
                {
                    dic["Result"] = model.Result;
                }
                dicList.Add(dic);
            }
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailsendfailure", dicList) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(EmailSendFailureEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailSendFailureID != null)
            {
                dic["EmailSendFailureID"] = model.EmailSendFailureID;
            }
            if (model.EmailID != null)
            {
                dic["EmailID"] = model.EmailID;
            }
            if (model.EmailBccAccountID != null)
            {
                dic["EmailBccAccountID"] = model.EmailBccAccountID;
            }
            if (model.EmailAccountID != null)
            {
                dic["EmailAccountID"] = model.EmailAccountID;
            }
            dic["EmailSendFailureSendTime"] = model.EmailSendFailureSendTime;
            if (model.Result != null)
            {
                dic["Result"] = model.Result;
            }
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailsendfailure", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(string EmailSendFailureID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailSendFailureID"] = EmailSendFailureID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailsendfailure", dic) > 0;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public T Get<T>(string EmailSendFailureID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailSendFailureID"] = EmailSendFailureID;
            return SQLHelperFactory.Instance.QueryForObjectByT<T>("Select_emailsendfailure", dic);
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
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<T>("Select_emailsendfailure_List", dic, out int total);
            return (list, total);
        }

    }
}
