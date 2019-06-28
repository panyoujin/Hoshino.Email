using System;
using System.Collections.Generic;
using Hoshino.Email.Entity;

namespace Hoshino.Email.IRepository
{
    public interface IEmailAccountRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        bool Insert(EmailAccountEntity model);

        /// <summary>
        /// 修改
        /// </summary>
        bool UpdateRemainCount(EmailAccountEntity model);

        /// <summary>
        /// 删除
        /// </summary>
        bool Delete(string EmailAccountID);

        /// <summary>
        /// 获取单个
        /// </summary>
        T Get<T>(string EmailAccountID);

        /// <summary>
        /// 获取列表
        /// </summary>
        (IEnumerable<T>,int) GetList<T>(int pageindex,int pagesize);

    }
}
