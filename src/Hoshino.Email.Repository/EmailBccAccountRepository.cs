using System;
using System.Collections.Generic;
using System.Linq;
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
            dic["EmailBccAccountAddress"] = model.EmailBccAccountAddress;
            dic["EmailBccAccountName"] = model.EmailBccAccountName;
            dic["EmailBccAccountCategoryID"] = model.EmailBccAccountCategoryID;
            dic["EmailBccAccountCategoryName"] = model.EmailBccAccountCategoryName;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailbccaccount", dic) > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public bool Update(EmailBccAccountEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailBccAccountID"] = model.EmailBccAccountID;
            dic["EmailBccAccountAddress"] = model.EmailBccAccountAddress;
            dic["EmailBccAccountName"] = model.EmailBccAccountName;
            dic["EmailBccAccountCategoryID"] = model.EmailBccAccountCategoryID;
            dic["EmailBccAccountCategoryName"] = model.EmailBccAccountCategoryName;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Update_emailbccaccount", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(int EmailBccAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailBccAccountID"] = EmailBccAccountID;
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailbccaccount", dic) > 0;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailBccAccountEntity Get(int EmailBccAccountID)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailBccAccountID"] = EmailBccAccountID;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailBccAccountEntity>("Select_emailbccaccount", dic);
        }
        /// <summary>
        /// 获取单个
        /// </summary>
        public EmailBccAccountEntity GetByAddress(string EmailBccAccountAddress)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["EmailBccAccountAddress"] = EmailBccAccountAddress;
            return SQLHelperFactory.Instance.QueryForObjectByT<EmailBccAccountEntity>("Select_EmailBccAccountByAddress", dic);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public (IEnumerable<EmailBccAccountEntity>, int) GetList(string address, string group, int pageindex, int pagesize)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(address))
            {
                dic["EmailBccAccountAddress"] = address;
            }
            if (!string.IsNullOrWhiteSpace(group))
            {
                EmailBccAccountCategoryRepository repository = new EmailBccAccountCategoryRepository();
                var idList = repository.GetIDList(group).Select(i => i.ID);
                dic["EmailBccAccountCategoryID"] = string.Join("','", idList);
            }
            if (pageindex >= 0)
            {
                dic["StartIndex"] = pageindex <= 1 ? 0 : (pageindex - 1) * pagesize;
            }
            if (pagesize > 0)
            {
                dic["SelectCount"] = pagesize;
            }
            var list = SQLHelperFactory.Instance.QueryMultipleByPage<EmailBccAccountEntity>("Select_emailbccaccount_ListByPage", dic, out int total);
            return (list, total);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public IEnumerable<EmailBccAccountEntity> GetBccEmailList(string address, string group)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(address))
            {
                dic["EmailBccAccountAddress"] = address;
            }
            if (!string.IsNullOrWhiteSpace(group))
            {
                EmailBccAccountCategoryRepository repository = new EmailBccAccountCategoryRepository();
                var idList = repository.GetIDList(group).Select(i => i.ID);
                dic["EmailBccAccountCategoryID"] = string.Join("','", idList);
            }
            return SQLHelperFactory.Instance.QueryForListByT<EmailBccAccountEntity>("Select_emailbccaccount_All", dic);
        }


        /// <summary>
        /// 判斷temp表是否有數據
        /// </summary>
        public bool ExistsEmailbccaccountTemp()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var f = SQLHelperFactory.Instance.QueryForObjectByT<string>("Exists_emailbccaccount_temp", dic);
            return !string.IsNullOrWhiteSpace(f);
        }

        /// <summary>
        /// 新增
        /// </summary>
        public bool InsertTemp(List<EmailBccAccountEntity> list)
        {
            List<Dictionary<string, object>> dics = new List<Dictionary<string, object>>();
            foreach (var model in list)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    ["EmailBccAccountAddress"] = model.EmailBccAccountAddress,
                    ["EmailBccAccountName"] = model.EmailBccAccountName,
                    ["EmailBccAccountCategoryName"] = model.EmailBccAccountCategoryName
                };
                dics.Add(dic);
                if (dics.Count > 1000)
                {
                    SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailbccaccount_temp", dics);
                    dics.Clear();
                }
            }
            if (dics.Count > 0)
            {
                SQLHelperFactory.Instance.ExecuteNonQuery("Insert_emailbccaccount_temp", dics);
            }
            return true;
        }

        /// <summary>
        /// 複製臨時表數據到正式表
        /// </summary>
        public bool CopyEmailBccAccount()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            return SQLHelperFactory.Instance.ExecuteNonQuery("copy_emailbccaccount_temp", dic) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool DeleteTemp()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            return SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailbccaccount_temp", dic) > 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        public bool ImportDelete()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var ids = SQLHelperFactory.Instance.QueryForListByT<int>("Get_Delete_emailbccaccount_import", dic);
            while (ids != null && ids.Count > 0)
            {
                if (ids != null && ids.Count > 0)
                {
                    dic["EmailBccAccountID"] = string.Join(",", SQLHelperFactory.Instance.QueryForListByT<int>("Get_Delete_emailbccaccount_import", dic));
                    SQLHelperFactory.Instance.ExecuteNonQuery("Delete_emailbccaccount_import", dic);
                }
                ids = SQLHelperFactory.Instance.QueryForListByT<int>("Get_Delete_emailbccaccount_import", dic);

            }
            DeleteTemp();
            return true;
        }
    }
}
