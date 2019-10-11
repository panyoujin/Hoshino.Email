using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Hoshino.Email.Core
{
    public class NetHelper
    {
        #region 获取本机的局域网IP
        /// <summary>
        /// 获取本机的局域网IP
        /// </summary>        
        public static string LANIP
        {
            get
            {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //如果本机IP列表为空，则返回空字符串
                if (addressList.Length < 1)
                {
                    return "";
                }

                //返回本机的局域网IP
                return addressList[addressList.Length-1].ToString();
            }
        }
        #endregion
    }
}
