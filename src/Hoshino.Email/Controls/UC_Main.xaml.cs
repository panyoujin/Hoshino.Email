using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hoshino.Email.Controls
{
    /// <summary>
    /// UC_Main.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Main : UserControl
    {
        public UC_Main()
        {
            InitializeComponent();

            List<EmailInfo> EmailInfoList = new List<EmailInfo>();
            EmailInfoList.Add(new EmailInfo { Num = 1, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "空閑", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 2, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "發送中",Color= "#FFEE4545", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 3, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "空閑", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 4, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "空閑", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 5, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "空閑", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 1, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "空閑", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 2, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "發送中", Color = "#FFEE4545", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 3, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "空閑", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 4, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "空閑", CreateTime = DateTime.Now.ToString() });
            EmailInfoList.Add(new EmailInfo { Num = 5, Email = "54f5dfsdf@qq.com", Group = "分類1", Status = "空閑", CreateTime = DateTime.Now.ToString() });

            dgEmail.ItemsSource = EmailInfoList;
        }

        public class EmailInfo
        {
            public int Num { set; get; }
            public string Email { set; get; }
            public string Group { set; get; }
            public string Status { set; get; }
            public string Color { set; get; } = "#FF65CB65";
            public string CreateTime { set; get; }
        }
    }
}
