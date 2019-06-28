using System;

namespace Hoshino.Email.Entity
{
    public class EmailSendBccAccountEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string EmailSendBccAccountID { get; set; }

        /// <summary>
        /// 邮件ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// 密送邮箱ID
        /// </summary>
        public string EmailBccAccountID { get; set; }

        /// <summary>
        /// 发件箱ID
        /// </summary>
        public string EmailAccountID { get; set; }

        /// <summary>
        /// 1:已发送;0:未发送;-1:发送失败
        /// </summary>
        public int? EmailSendBccAccountState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EmailSendBccAccountCreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? EmailSendBccAccountLastTime { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? EmailSendBccAccountSendTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string EmailBccAccountAddress { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string EmailBccAccountName { get; set; }

        public int EmailBccAccountIsDel { get; set; }
    }
}
