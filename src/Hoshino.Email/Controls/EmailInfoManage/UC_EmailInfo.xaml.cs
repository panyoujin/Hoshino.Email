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

namespace Hoshino.Email.Controls.EmailInfoManage
{
    /// <summary>
    /// UC_EmailInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UC_EmailInfo : UserControl
    {
        public UC_EmailInfo()
        {
            InitializeComponent();
        }



        private void BtnReceiptList_Click(object sender, RoutedEventArgs e)
        {
            new Win_ReceiptList().Show();
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            new Win_SentList().Show();
        }

        private void BtnSentList_Click(object sender, RoutedEventArgs e)
        {
            new Win_SentList().Show();
        }
    }
}
