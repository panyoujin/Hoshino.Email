using System;
using System.Collections.Generic;
using DBHelper.SQLHelper;
using Hoshino.Email.Entity;

namespace Hoshino.Email.Repository
{
    public class EmailInfoRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        public bool Insert(EmailInfoEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailID != null)
            {
                dic["EmailID"] = model.EmailID;
            }
            if (model.EmailTitle != null)
            {
                dic["EmailTitle"] = model.EmailTitle;
            }
            dic["EmailCreateTime"] = model.EmailCreateTime;
            dic["EmailLastTime"] = model.EmailLastTime;
            if (model.EmailFilePath != null)
            {
                dic["EmailFilePath"] = model.EmailFilePath;
            }
            dic["EmailIsDel"] = model.EmailIsDel;
            dic["EmailState"] = model.EmailState;
            dic["EmailStartSendTime"] = model.EmailStartSendTime;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailinfo", dic) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(EmailInfoEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailID != null)
            {
                dic["EmailID"] = model.EmailID;
            }
            if (model.EmailTitle != null)
            {
                dic["EmailTitle"] = model.EmailTitle;
            }
            dic["EmailCreateTime"] = model.EmailCreateTime;
            dic["EmailLastTime"] = model.EmailLastTime;
            if (model.EmailFilePath != null)
            {
                dic["EmailFilePath"] = model.EmailFilePath;
            }
            dic["EmailIsDel"] = model.EmailIsDel;
            dic["EmailState"] = model.EmailState;
            dic["EmailStartSendTime"] = model.EmailStartSendTime;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailinfo", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(string EmailID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailID"] = EmailID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailinfo", dic) > 0;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailInfoEntity Get(string EmailID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailID"] = EmailID;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailInfoEntity>("Select_emailinfo", dic);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public (IEnumerable<EmailInfoEntity>, int) GetList(string name, int pageindex, int pagesize)
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
            if (!string.IsNullOrWhiteSpace(name))
            {
                dic["EmailTitle"] = name;
            }
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<EmailInfoEntity>("Select_emailinfo_List_ByPage", dic, out int total);
            return (list, total);
        }

        /// <summary>
        /// 根据邮箱ID 获取下一个发送的邮件信息
        /// </summary>
        public EmailInfoEntity GetNextSendEmailInfoByEmailAccount(string EmailAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailAccountID"] = EmailAccountID;
            //先把已经完成的状态进行修改
            SQLHelperFactory.Instance.ExecuteNonQuery("Update_EmailInfo_EmailState", dic);
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailInfoEntity>("Select_NextSendEmailInfoByEmailAccount", dic);
        }

    }
}
