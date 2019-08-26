using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshino.Email.Core
{
    public class Enum
    {
    }

    /// <summary>
    /// 系統的功能枚舉
    /// </summary>
    public enum SysFunction
    {
        /// <summary>
        /// 首頁
        /// </summary>
        HomePage = 0,
        /// <summary>
        /// 通訊錄
        /// </summary>
        AddressBook = 1,
        /// <summary>
        /// 郵件管理
        /// </summary>
        MailManagement = 2,
        /// <summary>
        /// 新增郵件
        /// </summary>
        NewMail = 3,
        /// <summary>
        /// 郵件篩選
        /// </summary>
        MailScreening = 4,
    }
}
