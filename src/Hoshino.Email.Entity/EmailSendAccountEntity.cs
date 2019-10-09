using System;

namespace Hoshino.Email.Entity
{
    public class EmailSendAccountEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmailSendAccountID { get; set; }

        /// <summary>
        /// 邮件ID
        /// </summary>
        public int EmailID { get; set; }

        /// <summary>
        /// 发件箱ID
        /// </summary>
        public int EmailAccountID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EmailSendAccountCreateTime { get; set; }

        /// <summary>
        /// 發件人
        /// </summary>
        public string EmailAccountAddress { set; get; }
        /// <summary>
        /// 郵件標題
        /// </summary>
        public string EmailTitle { set; get; }
    }
}
