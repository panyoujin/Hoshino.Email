using System;

namespace Hoshino.Email.Entity
{
    public class EmailBccAccountEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string EmailBccAccountID { get; set; }

        /// <summary>
        /// 收件箱地址
        /// </summary>
        public string EmailBccAccountAddress { get; set; }

        /// <summary>
        /// 收件人名称
        /// </summary>
        public string EmailBccAccountName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EmailBccAccountCreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EmailBccAccountLastTime { get; set; }

        /// <summary>
        /// 是否删除 1:已删除;0:未删除
        /// </summary>
        public int? EmailBccAccountIsDel { get; set; }

    }
}
