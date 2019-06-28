using System;

namespace Hoshino.Email.Entity
{
    public class EmailSendFailureEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string EmailSendFailureID { get; set; }

        /// <summary>
        /// 邮件ID
        /// </summary>
        public string EmailID { get; set; }

        /// <summary>
        /// 收件人ID
        /// </summary>
        public string EmailBccAccountID { get; set; }

        /// <summary>
        /// 发件人ID
        /// </summary>
        public string EmailAccountID { get; set; }

        /// <summary>
        /// 发送失败的时间
        /// </summary>
        public DateTime? EmailSendFailureSendTime { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string Result { get; set; }

    }
}
