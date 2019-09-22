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

    }
}
