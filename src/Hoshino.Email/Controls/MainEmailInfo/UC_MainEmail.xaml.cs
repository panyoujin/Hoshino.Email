using Hoshino.Email.Repository;
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

namespace Hoshino.Email.Controls.MainEmailInfo
{
    /// <summary>
    /// UC_MainEmail.xaml 的交互逻辑
    /// </summary>
    public partial class UC_MainEmail : UserControl
    {
        EmailAccountRepository EA_Repository = new EmailAccountRepository();
        int page = 2;
        int pagesize = 10;
        public UC_MainEmail()
        {
            InitializeComponent();
            this.Loaded += this.UC_MainEmail_Loaded;
        }

        private void UC_MainEmail_Loaded(object sender, RoutedEventArgs e)
        {
            GetList();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.page = 1;
            GetList();
        }

        private void GetList()
        {
            var EmailInfoList = new List<EmailInfo>();
            var (list, total) = EA_Repository.GetList(this.tbEmailAccount.Text, this.tbGroup.Text, page, pagesize);
            var start = (page - 1) * pagesize + 1;
            if (list != null && list.Count() > 0)
            {
                foreach (var info in list)
                {
                    EmailInfoList.Add(new EmailInfo { Num = start++, Email = info.EmailAccountAddress, Group = info.Group, Status = info.SendState == 0 ? "空闲" : "发送中", CreateTime = info.EmailAccountCreateTime.ToString() });
                }
            }

            dgEmail.ItemsSource = EmailInfoList;
        }
    }
}
