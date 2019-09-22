using System;

namespace Hoshino.Email.Entity
{
    public class EmailSendFailureEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmailSendFailureID { get; set; }

        /// <summary>
        /// 邮件ID
        /// </summary>
        public int EmailID { get; set; }

        /// <summary>
        /// 收件人ID
        /// </summary>
        public int EmailBccAccountID { get; set; }

        /// <summary>
        /// 发件人ID
        /// </summary>
        public int EmailAccountID { get; set; }

        /// <summary>
        /// 发送失败的时间
        /// </summary>
        public DateTime EmailSendFailureSendTime { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string Result { get; set; }

    }
}
