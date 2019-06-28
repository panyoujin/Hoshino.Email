using System;
using System.Collections.Generic;
using Hoshino.Email.Entity;

namespace Hoshino.Email.IRepository
{
    public interface IEmailSendFailureRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        bool Insert(EmailSendFailureEntity model);

        /// <summary>
        /// 修改
        /// </summary>
        bool Update(EmailSendFailureEntity model);

        /// <summary>
        /// 删除
        /// </summary>
        bool Delete(string EmailSendFailureID);

        /// <summary>
        /// 获取单个
        /// </summary>
        T Get<T>(string EmailSendFailureID);

        /// <summary>
        /// 获取列表
        /// </summary>
        (IEnumerable<T>,int) GetList<T>(int pageindex,int pagesize);

    }
}
