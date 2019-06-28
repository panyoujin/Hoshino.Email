using System;
using System.Collections.Generic;
using Hoshino.Email.Entity;

namespace Hoshino.Email.IRepository
{
    public interface IEmailBccAccountRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        bool Insert(EmailBccAccountEntity model);

        /// <summary>
        /// 修改
        /// </summary>
        bool Update(EmailBccAccountEntity model);

        /// <summary>
        /// 删除
        /// </summary>
        bool Delete(string EmailBccAccountID);

        /// <summary>
        /// 获取单个
        /// </summary>
        T Get<T>(string EmailBccAccountID);

        /// <summary>
        /// 获取列表
        /// </summary>
        (IEnumerable<T>,int) GetList<T>(int pageindex,int pagesize);

    }
}
