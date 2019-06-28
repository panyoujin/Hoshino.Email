using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Hoshino.Email.Core.Helper
{
    /// <summary>
    /// Xml帮助文档
    /// </summary>
    /// jimmy.pan ADD 2014/06/10
    public class XmlHelper
    {
        /// <summary>
        /// 从文件加载 System.Xml.Linq.XElement。
        /// </summary>
        /// <param name="xmlPath">xml路径</param>
        /// <returns></returns>
        /// <exception cref="xmlPath">System.ArgumentNullException异常
        /// xmlPath参数为null或者空字符串</exception>
        public static XElement XmlLoad(string xmlPath)
        {
            if (string.IsNullOrWhiteSpace(xmlPath))
            {
                throw new ArgumentNullException("xmlPath参数不能为空");
            }
            XElement xe = XElement.Load(xmlPath);
            return xe;
        }

        /// <summary>
        /// 返回具有指定 System.Xml.Linq.XName 的此 System.Xml.Linq.XElement 的 System.Xml.Linq.XAttribute。
        /// </summary>
        /// <param name="xe"></param>
        /// <param name="attribute"></param>
        /// <exception cref="xe">System.ArgumentNullException 参数xe不能为空</exception>
        /// <exception cref="attribute">System.ArgumentNullException 参数attribute不能为空</exception>
        /// <exception cref="">XmlAttributeNullException指定配置的value为空</exception>
        public static string GetAttribute(XElement xe, string attribute)
        {
            if (xe == null)
            {
                throw new ArgumentNullException("xe参数不能为空");
            }
            if (string.IsNullOrWhiteSpace(attribute))
            {
                throw new ArgumentNullException("attribute参数不能为空");
            }
            if (xe.Attribute(attribute) == null)
            {
                throw new Exception(string.Format(@"指定配置[{0}]不存在", attribute));
            }
            return xe.Attribute(attribute).Value;
        }

        /// <summary>
        /// 获取具有指定 System.Xml.Linq.XName 的第一个（按文档顺序）子元素。
        /// </summary>
        /// <param name="xe"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        /// <exception cref="xe">System.ArgumentNullException 参数xe不能为空</exception>
        /// <exception cref="attribute">System.ArgumentNullException 参数attribute不能为空</exception>
        /// <exception cref="">Exception指定配置的value为空</exception>
        public static string GetElement(XElement xe, string element)
        {
            if (xe == null)
            {
                throw new ArgumentNullException("xe参数不能为空");
            }
            if (string.IsNullOrWhiteSpace(element))
            {
                throw new ArgumentNullException("attribute参数不能为空");
            }
            if (xe.Element(element) == null)
            {
                throw new Exception(string.Format(@"指定配置[{0}]不存在", element));
            }
            return xe.Element(element).Value;
        }

        /// <summary>
        /// 获取具有指定 System.Xml.Linq.XName 的第一个（按文档顺序）子元素。
        /// </summary>
        /// <param name="xe"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        /// <exception cref="xe">System.ArgumentNullException 参数xe不能为空</exception>
        /// <exception cref="attribute">System.ArgumentNullException 参数attribute不能为空</exception>
        public static IEnumerable<XElement> GetElements(XElement xe, string element)
        {
            if (xe == null)
            {
                throw new ArgumentNullException("xe参数不能为空");
            }
            if (string.IsNullOrWhiteSpace(element))
            {
                throw new ArgumentNullException("attribute参数不能为空");
            }
            return xe.Elements(element);
        }
    }
}
