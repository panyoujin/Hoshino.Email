using System;
using System.Collections.Generic;
using DBHelper.SQLHelper;
using Hoshino.Email.Entity;

namespace Hoshino.Email.Repository
{
    public class EmailAccountCategoryRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        public bool Insert(string name)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["Name"] = name;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailaccountcategory", dic) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(EmailAccountCategoryEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["ID"] = model.ID;
            dic["Name"] = model.Name;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailaccountcategory", dic, true) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(int ID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["ID"] = ID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailaccountcategory", dic, true) > 0;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailAccountCategoryEntity Get(int ID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["ID"] = ID;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailAccountCategoryEntity>("Select_emailaccountcategory", dic);
        }


        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailAccountCategoryEntity Get(string name)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["Name"] = name;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailAccountCategoryEntity>("Select_emailaccountcategory_ByName", dic);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public (IEnumerable<EmailAccountCategoryEntity>, int) GetList(string name, int pageindex, int pagesize)
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
                dic["Name"] = name;
            }
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<EmailAccountCategoryEntity>("Select_emailaccountcategory_List_By_Page", dic, out int total);
            return (list, total);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public IEnumerable<EmailAccountCategoryEntity> GetList()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var list = SQLHelperFactory.Instance.QueryForListByT<EmailAccountCategoryEntity>("Select_emailaccountcategory_List", dic);
            return list;
        }

    }
}
