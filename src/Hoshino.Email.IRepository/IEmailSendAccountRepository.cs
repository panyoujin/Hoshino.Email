using System;
using System.Collections.Generic;
using Hoshino.Email.Entity;

namespace Hoshino.Email.IRepository
{
    public interface IEmailSendAccountRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        bool Insert(EmailSendAccountEntity model);

        /// <summary>
        /// 修改
        /// </summary>
        bool Update(EmailSendAccountEntity model);

        /// <summary>
        /// 删除
        /// </summary>
        bool Delete(string EmailSendAccountID);

        /// <summary>
        /// 获取单个
        /// </summary>
        T Get<T>(string EmailSendAccountID);

        /// <summary>
        /// 获取列表
        /// </summary>
        (IEnumerable<T>,int) GetList<T>(int pageindex,int pagesize);

    }
}
