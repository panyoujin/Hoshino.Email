using Hoshino.Email.Controls.SendEmailManage;
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

namespace Hoshino.Email.Controls.SendEmailManage
{
    /// <summary>
    /// UC_Main.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SendEmailManage : UserControl
    {
        public UC_SendEmailManage()
        {
            InitializeComponent();


        }

        private void BtnMainControl_Click(object sender, RoutedEventArgs e)
        {
            gBodyContent.Children.Clear();
            gBodyContent.Children.Add(new UC_SendEmail());
        }


        private void BtnGroupControl_Click(object sender, RoutedEventArgs e)
        {
            gBodyContent.Children.Clear();
            gBodyContent.Children.Add(new UC_MainGroup());
        }
    }
}
