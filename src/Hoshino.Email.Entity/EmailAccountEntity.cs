using System;

namespace Hoshino.Email.Entity
{
    public class EmailAccountEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmailAccountID { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int EmailAccountCategoryID { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string EmailAccountCategoryName { get; set; }

        /// <summary>
        /// 发件箱地址
        /// </summary>
        public string EmailAccountAddress { get; set; }

        /// <summary>
        /// 发件箱密码
        /// </summary>
        public string EmailAccountPassWord { get; set; }

        /// <summary>
        /// 发件人名称
        /// </summary>
        public string EmailAccountName { get; set; }

        /// <summary>
        /// SMTP信息
        /// </summary>
        public string EmailAccountSMTP { get; set; }

        /// <summary>
        /// SMTP端口
        /// </summary>
        public int EmailAccountSMTPPort { get; set; }

        /// <summary>
        /// POP3信息
        /// </summary>
        public string EmailAccountPOP3 { get; set; }

        /// <summary>
        /// POP3端口
        /// </summary>
        public int EmailAccountPOP3Port { get; set; }

        /// <summary>
        /// 是否SSL 1是 0否
        /// </summary>
        public int EmailAccountIsSSL { get; set; }

        /// <summary>
        /// 每次最大发送数量
        /// </summary>
        public int EmailAccountMaxEmailCount { get; set; }

        /// <summary>
        /// 下一次发送时间可发送剩余数量:用于可以充分利用资源
        /// </summary>
        public int EmailAccountRemainEmailCount { get; set; }

        /// <summary>
        /// 间隔时间：分钟
        /// </summary>
        public int EmailAccountSpace { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime EmailAccountCreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime EmailAccountLastTime { get; set; }

        /// <summary>
        /// 上一次发送时间
        /// </summary>
        public DateTime EmailAccountPreSendTime { get; set; }

        /// <summary>
        /// 下一次发送时间
        /// </summary>
        public DateTime EmailAccountNextSendTime { get; set; }

        /// <summary>
        /// 状态 0空闲，1正在发送， 2已发送
        /// </summary>
        public int SendState { get; set; }

        /// <summary>
        /// 0发送，1密送
        /// </summary>
        public int SendMode { get; set; }

        /// <summary>
        /// 占用服务器IP
        /// </summary>
        public string OccupyIP { get; set; }

        /// <summary>
        /// 占用时间
        /// </summary>
        public DateTime OccupyTime { get; set; }

        /// <summary>
        /// 帐号状态 1 有效 ; 0 删除
        /// </summary>
        public int EmailAccountState { get; set; }

    }
}
