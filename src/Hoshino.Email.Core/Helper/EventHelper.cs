using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hoshino.Email.Core.Helper
{
    public class EventHelper
    {
        public delegate void MyInvoke();

        public static Dictionary<string, MyInvoke> GlobalEvent = new Dictionary<string, MyInvoke>();

        public static void RegisteredEvent(string eventName, MyInvoke method)
        {
            if (!GlobalEvent.ContainsKey(eventName))
            {
                GlobalEvent.Add(eventName, method);
            }
        }

        public static void ExecuteEvent(string eventName)
        {
            if (GlobalEvent.ContainsKey(eventName))
            {
                GlobalEvent[eventName].Invoke();
            }
        }
        public static void RemoveEvent(string eventName)
        {
            if (GlobalEvent.ContainsKey(eventName))
            {
                GlobalEvent.Remove(eventName);
            }
        }

        public static void ClearEvent()
        {
            GlobalEvent.Clear();
        }
    }
}
