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
using System.Windows.Shapes;

namespace Hoshino.Email.Controls.SendEmailManage
{
    /// <summary>
    /// Win_NewMainGroup.xaml 的交互逻辑
    /// </summary>
    public partial class Win_NewMainGroup : Window
    {
        EmailAccountCategoryRepository EAC_Repository = new EmailAccountCategoryRepository();
        public Win_NewMainGroup()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.tbGroup.Text.Trim()))
            {
                MessageBox.Show("請輸入分類名稱");
                return;
            }
            EAC_Repository.Insert(this.tbGroup.Text.Trim());
            MessageBox.Show("成功");
            this.DialogResult = true;
            this.Close();
        }
    }
}
