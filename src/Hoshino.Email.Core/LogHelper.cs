using log4net;
using System;
using System.Diagnostics;
using System.Reflection;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Hoshino.Email.Core
{
    /// <summary>
    /// 基于log4net的日记功能
    /// </summary>
    /// jimmy.pan ADD 2019/09/08
    public class LogHelper
    {
        static readonly ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Debug(object message)
        {
            log.Debug(GetCallStackInfo() + message);
        }

        public static void Debug(object message, Exception exception)
        {
            log.Debug(GetCallStackInfo() + message, exception);
        }

        public static void Error(string message, Exception exception)
        {
            log.Error(GetCallStackDetail() + message, exception);
        }

        public static void Error(string modelType, string op, string message, Exception exception)
        {
            string temp = string.Format("模块名称:{0}\r\n操作：{1}\r\n消息：{2}\r\n", modelType, op, message);
            InternelError(temp, exception);
        }

        private static void InternelError(object message, Exception exception)
        {
            log.Error(GetCallStackDetail() + message, exception);
        }

        public static void Warn(object message)
        {
            log.Warn(GetCallStackInfo() + message);
        }

        public static void Warn(object message, Exception exception)
        {
            log.Warn(GetCallStackInfo() + message, exception);
        }

        public static void Info(object message)
        {
            log.Info(GetCallStackInfo() + message);
        }

        public static void Info(object message, Exception exception)
        {
            log.Info(GetCallStackInfo() + message, exception);
        }

        public static void Fatal(object message)
        {
            log.Fatal(GetCallStackDetail() + message);
        }

        public static void Fatal(object message, Exception exception)
        {
            log.Fatal(GetCallStackDetail() + message, exception);
        }


        private static string GetCallStackInfo()
        {
            return "";
            StackFrame callstack = new StackFrame(2, true);
            return " " + callstack.GetMethod().DeclaringType.FullName + ", " + callstack.GetMethod().Name + ", [L" + callstack.GetFileLineNumber() + "], ";
        }
        private static string GetCallStackDetail()
        {
            return "";
            //当前堆栈信息
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] sfs = st.GetFrames();
            //过虑的方法名称,以下方法将不会出现在返回的方法调用列表中
            string _filterdName = "ResponseWrite,ResponseWriteError,";
            string _fullName = string.Empty, _methodName = string.Empty;
            for (int i = 1; i < sfs.Length; ++i)
            {
                //非用户代码,系统方法及后面的都是系统调用，不获取用户代码调用结束
                if (System.Diagnostics.StackFrame.OFFSET_UNKNOWN == sfs[i].GetILOffset()) break;
                _methodName = sfs[i].GetMethod().Name;//方法名称
                                                      //sfs[i].GetFileLineNumber();//没有PDB文件的情况下将始终返回0
                if (_filterdName.Contains(_methodName)) continue;
                _fullName = _methodName + "()->" + _fullName;
            }
            st = null;
            sfs = null;
            _filterdName = _methodName = null;
            return _fullName.TrimEnd('-', '>');
        }
    }
}
