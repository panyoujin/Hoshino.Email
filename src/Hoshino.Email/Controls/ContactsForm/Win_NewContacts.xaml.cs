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

namespace Hoshino.Email.Controls.ContactsForm
{
    /// <summary>
    /// Win_NewMainGroup.xaml 的交互逻辑
    /// </summary>
    public partial class Win_NewContacts : Window
    {
        int EmailBccAccountID = -1;
        EmailBccAccountRepository EBA_Repository = new EmailBccAccountRepository();
        EmailBccAccountCategoryRepository EBAC_Repository = new EmailBccAccountCategoryRepository();
        public Win_NewContacts()
        {
            InitializeComponent();
            this.EmailBccAccountID = -1;
            this.cbCategory.ItemsSource = EBAC_Repository.GetList();
            this.cbCategory.DisplayMemberPath = "Name";
        }
        public Win_NewContacts(int EmailBccAccountID)
        {
            InitializeComponent();
            this.EmailBccAccountID = EmailBccAccountID;
            this.cbCategory.ItemsSource = EBAC_Repository.GetList();
            this.cbCategory.DisplayMemberPath = "Name";
            var entity = EBA_Repository.Get(this.EmailBccAccountID);
            this.tbAddress.Text = entity.EmailBccAccountAddress;
            this.cbCategory.SelectedValue = entity.EmailBccAccountCategoryID;
            this.cbCategory.Text = entity.EmailBccAccountCategoryName;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbAddress.Text.Trim()))
            {
                "請填寫完整再提交".ShowDialog();
                return;
            }
            var category = this.cbCategory.SelectedItem as EmailBccAccountCategoryEntity;
            var entity = EBA_Repository.GetByAddress(tbAddress.Text.Trim());
            if (this.EmailBccAccountID == -1 && entity != null)
            {
                "郵箱已存在，請不要重複添加".ShowDialog();
                return;
            }
            if (this.EmailBccAccountID != -1 && entity != null && this.EmailBccAccountID != entity.EmailBccAccountID)
            {
                "郵箱已存在".ShowDialog();
                return;
            }
            if (this.EmailBccAccountID == -1)
            {
                entity = new EmailBccAccountEntity
                {
                    EmailBccAccountAddress = tbAddress.Text.Trim(),
                    EmailBccAccountName = tbAddress.Text.Trim(),
                    EmailBccAccountCategoryID = category.ID,
                    EmailBccAccountCategoryName = category.Name
                };
                EBA_Repository.Insert(entity);
            }
            else
            {
                entity = EBA_Repository.Get(this.EmailBccAccountID);
                entity.EmailBccAccountAddress = tbAddress.Text.Trim();
                entity.EmailBccAccountName = tbAddress.Text.Trim();
                entity.EmailBccAccountCategoryID = category.ID;
                entity.EmailBccAccountCategoryName = category.Name;
                EBA_Repository.Update(entity);
            }
            "成功".ShowDialog();
            this.DialogResult = true;
            this.Close();
        }
    }
}
