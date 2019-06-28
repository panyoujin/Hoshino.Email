using System;

namespace Hoshino.Email.Entity
{
    public class EmailSendAccountEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string EmailSendAccountID { get; set; }

        /// <summary>
        /// 邮件ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// 发件箱ID
        /// </summary>
        public string EmailAccountID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EmailSendAccountCreateTime { get; set; }

    }
}
