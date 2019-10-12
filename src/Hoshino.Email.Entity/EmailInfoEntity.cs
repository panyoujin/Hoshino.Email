using System;
using System.Windows;

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
        /// 0:启动发送;1:发送完毕;2:草稿; 3:分配完毕；4:停止發送
        /// </summary>
        public int EmailState { get; set; }
        /// <summary>
        /// 發送狀態對應的文本
        /// </summary>
        public string EmailStateStr
        {
            get
            {
                string str = "";
                switch (EmailState)
                {
                    case 0:
                        str = "启动发送";
                        break;
                    case 1:
                        str = "发送完毕";
                        break;
                    case 2:
                        str = "草稿";
                        break;
                    case 3:
                        str = "分配完毕";
                        break;
                    case 4:
                        str = "已停止";
                        break;
                }
                return str;
            }
        }
        /// <summary>
        /// 启动发送的时间
        /// </summary>
        public DateTime? EmailStartSendTime { get; set; }

        /// <summary>
        /// 發送總數量
        /// </summary>
        public int TotalQty { get; set; }
        /// <summary>
        /// 已發送數量
        /// </summary>
        public int AlreadySendQty { get; set; }
        /// <summary>
        /// 失敗發送數量
        /// </summary>
        public int FailQty { get; set; }
        /// <summary>
        /// 還需要發送的數量
        /// </summary>
        public int NeedSentQty
        {
            get
            {
                return TotalQty - AlreadySendQty- FailQty;
            }
        }


        #region 操作按鈕相關邏輯
        /// <summary>
        /// 操作按鈕顯示的文本
        /// </summary>
        public string OperationContent
        {
            get
            {
                string str = "";
                switch (EmailState)
                {
                    case 0:
                        str = "停止發送";
                        break;
                    case 1:
                        str = "发送完毕";
                        break;
                    case 2:
                        str = "開始發送";
                        break;
                    case 3:
                        str = "停止發送";
                        break;
                    case 4:
                        str = "繼續發送";
                        break;
                }
                return str;
            }
        }

        public Visibility Visibility
        {
            get
            {
                
                if (EmailState == 1)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        #endregion
    }
}
