using System;
using System.Collections.Generic;
using Hoshino.Email.Entity;

namespace Hoshino.Email.IRepository
{
    public interface IEmailSendBccAccountRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        bool Insert(EmailSendBccAccountEntity model);

        /// <summary>
        /// 修改
        /// </summary>
        bool Update(EmailSendBccAccountEntity model);

        /// <summary>
        /// 删除
        /// </summary>
        bool Delete(string EmailSendBccAccountID);

        /// <summary>
        /// 获取单个
        /// </summary>
        T Get<T>(string EmailSendBccAccountID);

        /// <summary>
        /// 获取列表
        /// </summary>
        (IEnumerable<T>,int) GetList<T>(int pageindex,int pagesize);

    }
}
