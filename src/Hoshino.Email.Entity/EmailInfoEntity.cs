using System;

namespace Hoshino.Email.Entity
{
    public class EmailInfoEntity
    {
        /// <summary>
        /// 邮件ID
        /// </summary>
        public int EmailID { get; set; }

        /// <summary>
        /// 邮件标题
        /// </summary>
        public string EmailTitle { get; set; }

        /// <summary>
        /// 邮件创建时间
        /// </summary>
        public DateTime EmailCreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime EmailLastTime { get; set; }

        /// <summary>
        /// 正文內容
        /// </summary>
        public string EmailFilePath { get; set; }

        /// <summary>
        /// 是否删除 1:已删除;0:未删除
        /// </summary>
        public int EmailIsDel { get; set; }

        /// <summary>
        /// 0:启动发送;1:发送完毕;2:草稿; 3:分配完毕
        /// </summary>
        public int EmailState { get; set; }

        /// <summary>
        /// 启动发送的时间
        /// </summary>
        public DateTime? EmailStartSendTime { get; set; }

    }
}
