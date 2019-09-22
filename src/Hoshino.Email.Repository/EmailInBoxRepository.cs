using System;
using System.Collections.Generic;
using DBHelper.SQLHelper;
using Hoshino.Email.Entity;

namespace Hoshino.Email.Repository
{
    public class EmailInBoxRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        public bool Insert(EmailInBoxEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailInboxID != null)
            {
                dic["EmailInboxID"] = model.EmailInboxID;
            }
            if (model.EmailServerUID != null)
            {
                dic["EmailServerUID"] = model.EmailServerUID;
            }
            if (model.EmailInboxTitle != null)
            {
                dic["EmailInboxTitle"] = model.EmailInboxTitle;
            }
            if (model.EmailInboxFrom != null)
            {
                dic["EmailInboxFrom"] = model.EmailInboxFrom;
            }
            if (model.EmailInboxFromName != null)
            {
                dic["EmailInboxFromName"] = model.EmailInboxFromName;
            }
            dic["EmailInboxDate"] = model.EmailInboxDate;
            if (model.EmailInboxFilePath != null)
            {
                dic["EmailInboxFilePath"] = model.EmailInboxFilePath;
            }
            dic["EmailInboxState"] = model.EmailInboxState;
            dic["EmailInboxIsDel"] = model.EmailInboxIsDel;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailinbox", dic) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(EmailInBoxEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (model.EmailInboxID != null)
            {
                dic["EmailInboxID"] = model.EmailInboxID;
            }
            if (model.EmailServerUID != null)
            {
                dic["EmailServerUID"] = model.EmailServerUID;
            }
            if (model.EmailInboxTitle != null)
            {
                dic["EmailInboxTitle"] = model.EmailInboxTitle;
            }
            if (model.EmailInboxFrom != null)
            {
                dic["EmailInboxFrom"] = model.EmailInboxFrom;
            }
            if (model.EmailInboxFromName != null)
            {
                dic["EmailInboxFromName"] = model.EmailInboxFromName;
            }
            dic["EmailInboxDate"] = model.EmailInboxDate;
            if (model.EmailInboxFilePath != null)
            {
                dic["EmailInboxFilePath"] = model.EmailInboxFilePath;
            }
            dic["EmailInboxState"] = model.EmailInboxState;
            dic["EmailInboxIsDel"] = model.EmailInboxIsDel;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailinbox", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(string EmailInboxID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailInboxID"] = EmailInboxID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailinbox", dic) > 0;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public T Get<T>(string EmailInboxID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailInboxID"] = EmailInboxID;
            return SQLHelperFactory.Instance.QueryForObjectByT<T>("Select_emailinbox", dic);
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
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<T>("Select_emailinbox_List", dic, out int total);
            return (list, total);
        }

    }
}
