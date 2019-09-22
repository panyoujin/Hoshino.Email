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
        public int EmailAccountID = -1;
        EmailAccountRepository EA_Repository = new EmailAccountRepository();
        EmailAccountCategoryRepository EAC_Repository = new EmailAccountCategoryRepository();
        public Win_NewOneEmail()
        {
            InitializeComponent();
            this.EmailAccountID = -1;
            this.cbCategory.ItemsSource = EAC_Repository.GetList();
            this.cbCategory.DisplayMemberPath = "Name";
        }
        public Win_NewOneEmail(int EmailAccountID)
        {
            InitializeComponent();
            this.EmailAccountID = EmailAccountID;
            this.cbCategory.ItemsSource = EAC_Repository.GetList();
            this.cbCategory.DisplayMemberPath = "Name";
            var entity = EA_Repository.Get(this.EmailAccountID);
            this.txtName.Text = entity.EmailAccountName;
            this.txtEmailAddress.Text = entity.EmailAccountAddress;
            this.txtPassword.Text = entity.EmailAccountPassWord;
            this.txtInterval.Text = entity.EmailAccountSpace + "";
            this.txtMaxSendCount.Text = entity.EmailAccountMaxEmailCount + "";
            this.txtPOP3.Text = entity.EmailAccountPOP3;
            this.txtPOPPort.Text = entity.EmailAccountPOP3Port + "";
            this.txtSMTP.Text = entity.EmailAccountSMTP;
            this.txtSMTPPort.Text = entity.EmailAccountSMTPPort + "";
            this.cbCategory.SelectedValue = entity.EmailAccountCategoryID;
            this.cbCategory.Text = entity.EmailAccountCategoryName;
            this.cbSendMode.Text = entity.SendMode == 0 ? "發送" : "密送";
            this.cbSSL.SelectedIndex = entity.EmailAccountIsSSL;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text.Trim()) || string.IsNullOrWhiteSpace(txtEmailAddress.Text.Trim()) || string.IsNullOrWhiteSpace(txtPassword.Text.Trim()) || string.IsNullOrWhiteSpace(txtInterval.Text.Trim()) || string.IsNullOrWhiteSpace(txtMaxSendCount.Text.Trim()) || string.IsNullOrWhiteSpace(txtPOP3.Text.Trim()) || string.IsNullOrWhiteSpace(txtPOPPort.Text.Trim()) || string.IsNullOrWhiteSpace(txtSMTP.Text.Trim()) || string.IsNullOrWhiteSpace(txtSMTPPort.Text.Trim()))
            {
                "請填寫完整再提交".ShowDialog();
                return;
            }
            var category = this.cbCategory.SelectedItem as EmailAccountCategoryEntity;
            var entity = EA_Repository.GetByAddress(txtEmailAddress.Text.Trim());
            if (this.EmailAccountID == -1 && entity != null)
            {
                "郵箱已存在，請不要重複添加".ShowDialog();
                return;
            }
            if (this.EmailAccountID != -1 && entity != null && this.EmailAccountID != entity.EmailAccountID)
            {
                "郵箱已存在".ShowDialog();
                return;
            }
            if (this.EmailAccountID == -1)
            {
                entity = new EmailAccountEntity
                {
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
                    SendMode = (this.cbSendMode.Text == "發送" ? 0 : 1),
                    EmailAccountCategoryID = category.ID,
                    EmailAccountCategoryName = category.Name
                };
                EA_Repository.Insert(entity);
            }
            else
            {
                entity = EA_Repository.Get(this.EmailAccountID);
                entity.EmailAccountAddress = txtEmailAddress.Text.Trim();
                entity.EmailAccountAddress = txtEmailAddress.Text.Trim();
                entity.EmailAccountName = txtName.Text.Trim();
                entity.EmailAccountIsSSL = cbSSL.SelectedIndex;
                entity.EmailAccountMaxEmailCount = int.Parse(txtMaxSendCount.Text.Trim());
                entity.EmailAccountSpace = int.Parse(txtInterval.Text.Trim());
                entity.EmailAccountPassWord = txtPassword.Text.Trim();
                entity.EmailAccountPOP3 = txtPOP3.Text.Trim();
                entity.EmailAccountPOP3Port = int.Parse(txtPOPPort.Text.Trim());
                entity.EmailAccountSMTP = txtSMTP.Text.Trim();
                entity.EmailAccountSMTPPort = int.Parse(txtSMTPPort.Text.Trim());
                entity.SendMode = (this.cbSendMode.Text == "發送" ? 0 : 1);
                entity.EmailAccountCategoryID = category.ID;
                entity.EmailAccountCategoryName = category.Name;
                EA_Repository.Update(entity);
            }
            "成功".ShowDialog();
            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
