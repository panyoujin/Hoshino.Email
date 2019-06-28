using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Hoshino.Email.Core.Helper
{
    public class PingHelper
    {
        public static bool TryConnect(string ip, int timeout = 3000)
        {
            var result = false;
            try
            {
                using (Ping ping = new Ping { })
                {
                    var reply = ping.Send(ip, timeout);
                    if (reply.Status == IPStatus.Success)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error("PingHelper", "TryConnect", ex.Message, ex);
                result = false;
            }
            return result;
        }
    }
}
