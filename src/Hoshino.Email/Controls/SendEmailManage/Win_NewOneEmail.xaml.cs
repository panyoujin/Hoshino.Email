using Hoshino.Email.Entity;
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
    /// Win_NewOneEmail.xaml 的交互逻辑
    /// </summary>
    public partial class Win_NewOneEmail : Window
    {
        EmailAccountRepository EA_Repository = new EmailAccountRepository();
        public Win_NewOneEmail()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text.Trim()) || string.IsNullOrWhiteSpace(txtEmailAddress.Text.Trim()) || string.IsNullOrWhiteSpace(txtPassword.Text.Trim()) || string.IsNullOrWhiteSpace(txtInterval.Text.Trim()) || string.IsNullOrWhiteSpace(txtMaxSendCount.Text.Trim()) || string.IsNullOrWhiteSpace(txtPOP3.Text.Trim()) || string.IsNullOrWhiteSpace(txtPOPPort.Text.Trim()) || string.IsNullOrWhiteSpace(txtSMTP.Text.Trim()) || string.IsNullOrWhiteSpace(txtSMTPPort.Text.Trim()))
            {
                MessageBox.Show("請填寫完整再提交");
                return;
            }
            EmailAccountEntity entity = new EmailAccountEntity
            {
                EmailAccountID = Guid.NewGuid().ToString(),
                EmailAccountAddress = txtEmailAddress.Text.Trim(),
                EmailAccountName = txtName.Text.Trim(),
                EmailAccountIsSSL = cbSSL.SelectedIndex,
                EmailAccountMaxEmailCount = int.Parse(txtMaxSendCount.Text.Trim()),
                EmailAccountSpace = int.Parse(txtInterval.Text.Trim()),
                EmailAccountPassWord = txtPassword.Text.Trim(),
                EmailAccountPOP3 = txtPOP3.Text.Trim(),
                EmailAccountPOP3Port = int.Parse(txtPOPPort.Text.Trim()),
                EmailAccountSMTP = txtSMTP.Text.Trim(),
                EmailAccountSMTPPort = int.Parse(txtSMTPPort.Text.Trim()),
                SendMode = (this.cbSendMode.Text == "發送" ? 0 : 1)
            };
            EA_Repository.Insert(entity);
        }
    }
}
