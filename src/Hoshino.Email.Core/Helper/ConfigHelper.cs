﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshino.Email.Core.Helper
{
    /// <summary>
    /// 解析项目配置文件的帮助类
    /// </summary>
    public class ConfigHelper
    {
        public static T GetConfigValue<T>(string key, string defaultValue) where T : class
        {
            Type objType = typeof(T);
            var obj = Convert.ChangeType(ConfigurationManager.AppSettings[key] ?? defaultValue, objType);
            return obj as T;
        }

        public static string GetConfigValue(string key, string defaultValue = "")
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }
    }
}
