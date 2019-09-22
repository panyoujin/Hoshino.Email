using Hoshino.Email.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExt
    {
        public static void ShowDialog(this string msg)
        {
            WinDialog dialog = new WinDialog(msg);
            dialog.ShowDialog();
        }
        public static void ShowYesOrNoDialog(this string msg, Action yesAction = null, Action noAction = null)
        {
            WinDialog dialog = new WinDialog(msg);
            if (dialog.ShowDialog().Value)
            {
                yesAction?.Invoke();
            }
            else
            {
                noAction?.Invoke();
            }
        }
    }
}
