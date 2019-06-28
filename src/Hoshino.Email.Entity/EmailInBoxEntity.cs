using System;

namespace Hoshino.Email.Entity
{
    public class EmailInBoxEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string EmailInboxID { get; set; }

        /// <summary>
        /// 服务器ID
        /// </summary>
        public string EmailServerUID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string EmailInboxTitle { get; set; }

        /// <summary>
        /// 发件人地址
        /// </summary>
        public string EmailInboxFrom { get; set; }

        /// <summary>
        /// 发件人名称
        /// </summary>
        public string EmailInboxFromName { get; set; }

        /// <summary>
        /// 收件时间
        /// </summary>
        public int? EmailInboxDate { get; set; }

        /// <summary>
        /// eml地址
        /// </summary>
        public string EmailInboxFilePath { get; set; }

        /// <summary>
        /// 状态1:已阅读;0:未阅读
        /// </summary>
        public int? EmailInboxState { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int? EmailInboxIsDel { get; set; }

    }
}
