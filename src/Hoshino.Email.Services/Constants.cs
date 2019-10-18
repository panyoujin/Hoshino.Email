using Hoshino.Email.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hoshino.Email.Services
{
    public class Constants
    {
        public static List<string> ErrorContinueList;
        public static int SendWaitTime = 10;
        static Constants()
        {
            int.TryParse(ConfigHelper.GetConfigValue("SendWaitTime", "10"), out SendWaitTime);
            ErrorContinueList = ConfigHelper.GetConfigValue("ErrorContinue").Split('#').ToList();
            ErrorContinueList.RemoveAll(s => string.IsNullOrWhiteSpace(s));
        }

        public static void SleepInterval(int interval)
        {
            while (interval > 0)
            {
                Thread.Sleep((interval > 5 ? 5 : interval) * 1000);
                interval -= 5;
            }
        }
    }
}
